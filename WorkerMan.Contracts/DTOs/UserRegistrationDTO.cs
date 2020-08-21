namespace WorkerMan.Contracts.DTOs
{
    public class UserRegistrationDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string HomeAddress { get; set; }
        public string OfficeAddress { get; set; }
        public bool IsAdministratorAccount { get; set; }
    }
}
