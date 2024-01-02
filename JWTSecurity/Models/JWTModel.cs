namespace JwtSecurity.Models
{
    public interface IJWTModel
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }

    public class JWTModel : IJWTModel
    {
        public JWTModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
