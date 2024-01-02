namespace ApiClient.Models
{
    public class JWTModel
    {
        public JWTModel()
        {
            
        }
        public JWTModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
