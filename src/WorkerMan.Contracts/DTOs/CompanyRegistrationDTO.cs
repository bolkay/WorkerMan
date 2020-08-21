namespace WorkerMan.Contracts.DTOs
{
    public class CompanyRegistrationDTO
    {
        public string OfficialName { get; set; }
        public string OfficialAddress { get; set; }
        public string OfficialEmail { get; set; }
        public byte[] CompanyLogoData { get; set; }
        public string CompanyLogoName { get; set; }
        public UserRegistrationDTO UserRegistrationDTO { get; set; }
    }
}
