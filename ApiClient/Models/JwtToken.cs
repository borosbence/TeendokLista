namespace ApiClient.Models
{
    public class JwtToken
    {
        public JwtToken()
        {

        }
        public JwtToken(string accessToken, string refreshToken)
        {
            Access_Token = accessToken;
            Refresh_Token = refreshToken;
        }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
