using System.IO;
using System.Threading.Tasks;
using WorkerMan.Contracts.DTOs;
using WorkerMan.Contracts.Results;
using WorkerMan.CrossCutting.Enums;

namespace WorkerMan.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserRegistrationResult> RegisterUserAccountAsync(UserRegistrationDTO userRegistrationDTO);
        Task<UserLoginResult> LoginAsync(UserLoginDTO userLoginDTO);
        Task<bool> UploadProfilePicture(Stream stream, string fileName, string email);
        Task<UserDTO> GetUserByEmail(string email);
        Task<string> GetUserAccountType(string email);
    }
}
