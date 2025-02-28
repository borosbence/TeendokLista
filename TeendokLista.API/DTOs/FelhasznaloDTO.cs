using JwtSecurity.Models;

namespace TeendokLista.API.DTOs
{
    public class FelhasznaloDTO
    {
        public FelhasznaloDTO(int id, string felhasznalonev, string szerepkor)
        {
            Id = id;
            Felhasznalonev = felhasznalonev;
            Szerepkor = szerepkor;
        }

        public int Id { get; set; }
        public string Felhasznalonev { get; set; } = null!;
        public string Szerepkor { get; set; } = null!;
    }

    public class LoginResultDTO : FelhasznaloDTO, IJwtModel
    {
        public LoginResultDTO(int id, string felhasznalonev, string szerepkor, 
            string accessToken, string refreshToken) : base(id, felhasznalonev, szerepkor)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}