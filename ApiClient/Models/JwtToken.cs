namespace ApiClient.Models
{
    public class JwtToken
    {
        public JwtToken()
        {
            Access_Token = LoggedUser.Current.Access_Token;
            Refresh_Token = LoggedUser.Current.Refresh_Token;
        }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
