using System.ComponentModel.DataAnnotations;

namespace JWTSecurity.Models
{
    public class JwtToken
    {
        [Required]
        public string Access_Token { get; set; } = null!;
        [Required]
        public string Refresh_Token { get; set; } = null!;
    }
}
