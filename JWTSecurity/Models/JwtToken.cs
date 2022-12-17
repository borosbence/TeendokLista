namespace JwtSecurity.Models
{
    public class JwtToken
    {
        public string Access_Token { get; set; } = null!;
        public string Refresh_Token { get; set; } = null!;
    }
}
