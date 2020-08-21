namespace WorkerMan.Services.Configuration
{
    public class WorkerManIdentityOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double TokenExpirationInMinutes { get; set; }
    }
}
