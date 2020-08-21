using System;

namespace WorkerMan.Contracts.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AccountType { get; set; }
    }
}
