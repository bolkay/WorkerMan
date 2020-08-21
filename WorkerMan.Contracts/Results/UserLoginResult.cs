using WorkerMan.Contracts.DTOs;

namespace WorkerMan.Contracts.Results
{
    public class UserLoginResult : BaseUserResult
    {
        public string Token { get; set; }
        public int ValidFor { get; set; }
    }
}
