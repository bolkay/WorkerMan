using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkerMan.Contracts.DTOs;
using WorkerMan.Services.Interfaces;

namespace WorkerMan.API.Controllers.Area.User
{
    [Area("User")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterUserAccountAsync(userRegistrationDTO);

                if (result.Errors != null)
                {
                    if (result.Errors.Any())
                        return UnprocessableEntity(result.Errors);
                }
                else
                    return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginAsync(userLoginDTO);

                return result != null ? Ok(result) : UnprocessableEntity(new { userLoginDTO, Message = "Unable to process request" }) as IActionResult;
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserDetails([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest($"Invalid value for email. Your input : {email}.");

            var details = await userService.GetUserByEmail(email);

            return details != null ? Ok(details) : NotFound(email) as IActionResult;
        }
        [Authorize]
        [HttpGet("usertype")]
        public async Task<IActionResult> GetUserAccountType([FromQuery]string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest($"Invalid value for email. Your input : {email}.");

            var accountType = await userService.GetUserAccountType(email);

            return !string.IsNullOrEmpty(accountType) ? Ok(accountType) : NotFound(email) as IActionResult;
        }
    }
}
