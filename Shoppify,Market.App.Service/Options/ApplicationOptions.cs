namespace Shoppify.Market.App.Service.Options
{
    public class ApplicationOptions
    {
        public string SiteTitle { get; set; }
        public string SiteDescription { get; set; }
        public JwtConfig JwtConfig { get; set; }
    }

    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public string EncryptKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
