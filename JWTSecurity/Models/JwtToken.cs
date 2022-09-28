using System.ComponentModel.DataAnnotations;

namespace JwtSecurity.Models
{
    public class JwtToken
    {
        [Required]
        public string Access_Token { get; set; } = null!;
        [Required]
        public string Refresh_Token { get; set; } = null!;
    }
}
