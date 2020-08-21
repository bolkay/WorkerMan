using WorkerMan.Contracts.DTOs;

namespace WorkerMan.Contracts.Results
{
    public abstract class BaseUserResult
    {
        public UserDTO UserDTO { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
