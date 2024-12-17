using JwtSecurity.Models;

namespace TeendokLista.API.DTOs
{
    public class FelhasznaloDTO
    {
        public int Id { get; set; }
        public string Felhasznalonev { get; set; } = null!;
        public string Szerepkor { get; set; } = null!;
    }

    public class LoginResultDTO : FelhasznaloDTO, IJwtModel
    {
        public LoginResultDTO(int id, string felhasznalonev, string szerepkor, string accessToken, string refreshToken)
        {
            Id = id;
            Felhasznalonev = felhasznalonev;
            Szerepkor = szerepkor;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}