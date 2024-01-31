namespace ApiClient.Models
{
    public class JwtModel
    {
        public JwtModel()
        {
            
        }
        public JwtModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
