namespace JwtSecurity.Models
{
    public interface IJwtModel
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }

    public class JwtModel : IJwtModel
    {
        public JwtModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
