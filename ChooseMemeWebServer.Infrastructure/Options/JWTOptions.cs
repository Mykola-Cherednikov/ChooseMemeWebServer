namespace ChooseMemeWebServer.Infrastructure.Options
{
    public class JWTOptions
    {
        public const string SectionName = "JWT";

        public string SecretKey { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int ExpiryInMinutes { get; set; }
    }
}
