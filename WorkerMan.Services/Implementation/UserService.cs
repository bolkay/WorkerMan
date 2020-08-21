using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkerMan.Business.Interfaces;
using WorkerMan.Contracts.DTOs;
using WorkerMan.Contracts.Results;
using WorkerMan.CrossCutting.Contexts;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.CrossCutting.Enums;
using WorkerMan.Services.Configuration;
using WorkerMan.Services.Interfaces;
using WorkerMan.Storage.Impl;

namespace WorkerMan.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<WorkerManUser> userManager;
        private readonly SignInManager<WorkerManUser> signInManager;
        private readonly WorkerManIdentityOptions workerManIdentityOptions;
        private readonly IMapper mapper;
        private readonly ICompanyBusiness companyBusiness;
        private readonly FirebaseStorageManager firebaseStorageManager;
        private readonly WorkerManContext workerManContext;

        public UserService(UserManager<WorkerManUser> userManager, SignInManager<WorkerManUser> signInManager,
            WorkerManIdentityOptions workerManIdentityOptions, IMapper mapper, ICompanyBusiness companyBusiness
            , FirebaseStorageManager firebaseStorageManager, WorkerManContext workerManContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.workerManIdentityOptions = workerManIdentityOptions;
            this.mapper = mapper;
            this.companyBusiness = companyBusiness;
            this.firebaseStorageManager = firebaseStorageManager;
            this.workerManContext = workerManContext;
        }

        public async Task<string> GetUserAccountType(string email)
        {
            UserDTO userDTO = await GetUserByEmail(email);

            return userDTO != null ? userDTO.AccountType : null;
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            WorkerManUser workerManUser = await userManager.FindByEmailAsync(email);

            return mapper.Map<UserDTO>(workerManUser);
        }

        public async Task<UserLoginResult> LoginAsync(UserLoginDTO userLoginDTO)
        {
            UserLoginResult userLoginResult = null;
            if (userLoginDTO != null)
            {
                WorkerManUser workerManUser = await userManager.FindByEmailAsync(userLoginDTO.Email);

                var tryCheck = await signInManager.PasswordSignInAsync(workerManUser, userLoginDTO.Password,
                    userLoginDTO.RememberMe, false);

                if (tryCheck.Succeeded)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,workerManUser.FirstName ,ClaimValueTypes.String),
                        new Claim(ClaimTypes.Name,workerManUser.LastName ,ClaimValueTypes.String),
                        new Claim(ClaimTypes.Email,workerManUser.Email,ClaimValueTypes.Email),
                        new Claim(ClaimTypes.DateOfBirth,workerManUser.DateOfBirth.ToString(),ClaimValueTypes.Date)
                    };

                    byte[] key = Encoding.UTF8.GetBytes(workerManIdentityOptions.Key);
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);

                    SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                        issuer: workerManIdentityOptions.Issuer,
                        audience: workerManIdentityOptions.Audience,
                        claims: claims,
                        null,
                        expires: DateTime.Now.AddMinutes(workerManIdentityOptions.TokenExpirationInMinutes),
                        signingCredentials: signingCredentials);

                    userLoginResult = new UserLoginResult
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        ValidFor = jwtSecurityToken.ValidTo.Minute,
                        UserDTO = mapper.Map<UserDTO>(workerManUser)
                    };

                    return userLoginResult;
                }
            }

            return userLoginResult;
        }

        public async Task<UserRegistrationResult> RegisterUserAccountAsync(UserRegistrationDTO userRegistrationDTO)
        {
            UserRegistrationResult userRegistrationResult = null;
            if (userRegistrationDTO != null)
            {
                WorkerManUser workerManUser = mapper.Map<WorkerManUser>(userRegistrationDTO);

                if (null != workerManUser)
                {
                    if (userRegistrationDTO.IsAdministratorAccount)
                        workerManUser.AccountType = CrossCutting.Enums.AccountType.Administrator;
                    else
                        workerManUser.AccountType = CrossCutting.Enums.AccountType.Worker;

                    var tryCreateAccount = await userManager.CreateAsync(workerManUser, userRegistrationDTO.Password);

                    if (tryCreateAccount.Succeeded)
                    {
                        userRegistrationResult = new UserRegistrationResult
                        {
                            UserDTO = mapper.Map<UserDTO>(workerManUser),
                            Message = "Success",
                            Success = true
                        };
                    }
                    else
                    {
                        userRegistrationResult = new UserRegistrationResult
                        {
                            Errors = tryCreateAccount.Errors.Select(x => x.Description).ToList()
                        };
                    }
                }
            }
            return userRegistrationResult;
        }

        public async Task<bool> UploadProfilePicture(Stream stream, string fileName, string email)
        {
            WorkerManUser workerManUser = await userManager.FindByEmailAsync(email);
            if (workerManUser != null)
            {
                //Try upload
                string photoPath = await firebaseStorageManager.UploadPhoto(stream, fileName);

                if (!string.IsNullOrEmpty(photoPath))
                {
                    workerManUser.ProfilePicture = photoPath;

                    await workerManContext.SaveChangesAsync();

                    return true;
                }
            }
            return false;
        }
    }
}
