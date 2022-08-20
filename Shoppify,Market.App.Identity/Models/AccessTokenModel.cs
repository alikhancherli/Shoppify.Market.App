namespace Shoppify.Market.App.Identity.Models
{
    public class AccessTokenModel
    {
        public DateTime ExpirationTime { get; set; }
        public string? AccessToken { get; set; }
        public string TokenType { get; } = "Bearer";
    }
}
