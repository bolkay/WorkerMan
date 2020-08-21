namespace WorkerMan.Contracts.DTOs
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string CompanyAffiliation { get; set; }
    }
}
