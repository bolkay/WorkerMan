using System.Collections.Generic;
using WorkerMan.Contracts.DTOs;

namespace WorkerMan.Contracts.Results
{
    public class UserRegistrationResult : BaseUserResult
    {
        public List<string> Errors { get; set; }
    }
}
