using JwtSecurity.Models;
using JwtSecurity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeendokLista.API.Data;
using TeendokLista.API.DTOs;
using TeendokLista.API.Models;

namespace TeendokLista.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TeendokContext _context;
        private readonly JwtManagerService _jwtManagerService;

        public TokenController(TeendokContext context, JwtManagerService jwtManagerService)
        {
            _context = context;
            _jwtManagerService = jwtManagerService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResultDTO>> Login(LoginDTO userLogin)
        {
            // Felhasználó kikeresése
            var dbUser = await _context.felhasznalok
                .Include(x => x.szerepkor)
                .FirstOrDefaultAsync(x => x.felhasznalonev == userLogin.Username);
            // Ha nem létezik a felhasználó
            if (dbUser == null)
            {
                return Unauthorized("A felhasználónév nincs regisztrálva.");
            }
            // Jelszó hashelés
            // string password = BCrypt.Net.BCrypt.HashPassword(userLogin.Password);
            // Jelszó ellenőrzése
            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, dbUser.jelszo))
            {
                return Unauthorized("Hibás felhasználónév vagy jelszó.");
            }

            // Követelési szintek létrehozása
            var claims = GetClaimsFromUser(dbUser);
            // Új Token generálása
            var jwtToken = _jwtManagerService.GenerateToken(claims);
            // Refresh token elmentése az adatbázisba
            _context.login_tokenek.Add(new LoginToken(jwtToken.RefreshToken, dbUser.id));
            await _context.SaveChangesAsync();

            // Felhasználói adatok és token visszaadása
            return new LoginResultDTO(dbUser.id, dbUser.felhasznalonev, dbUser.szerepkor!.nev, jwtToken.AccessToken, jwtToken.RefreshToken);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Refresh")]
        public async Task<ActionResult<JWTModel>> Refresh(JWTModel jwtToken)
        {
            // Felhasználói adatok kinyerése a tokenből
            var principal = _jwtManagerService.GetPrincipalFromExpiredToken(jwtToken.AccessToken);
            var claimId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int.TryParse(claimId!.Value, out int userId);

            var dbUser = await _context.felhasznalok
                .Include(x => x.szerepkor)
                .FirstOrDefaultAsync(x => x.id == userId);
            if (dbUser == null)
            {
                return Unauthorized("A felhasználónév nincs regisztrálva.");
            }
            // Token kikeresése
            var oldToken = await _context.login_tokenek
                .FirstOrDefaultAsync(x => x.felhasznalo_id == dbUser.id && x.token == jwtToken.RefreshToken);
            if (oldToken == null)
            {
                return BadRequest("Érvénytelen token.");
            }
            // Ha nem egyezik a refresh token vagy már lejárt
            if (oldToken.token != jwtToken.RefreshToken || oldToken.lejarat_datum <= DateTime.Now)
            {
                // Régi lejárt token törlése
                // _context.login_tokenek.Remove(oldToken);
                return Unauthorized("Lejárt vagy érvénytelen token.");
            }
            // TODO: MAUI program még a régi refresh tokent küldi
            // Új token generálása
            var claims = GetClaimsFromUser(dbUser);
            var newToken = _jwtManagerService.GenerateToken(claims);
            // Refresh token elmentése az adatbázisba
            _context.login_tokenek.Add(new LoginToken(newToken.RefreshToken, dbUser.id));
            await _context.SaveChangesAsync();

            // Új token érték visszaadása
            return newToken;
        }


        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(JWTModel jwtToken)
        {
            var claimId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int.TryParse(claimId!.Value, out int userId);

            var dbUser = await _context.felhasznalok
                .FirstOrDefaultAsync(x => x.id == userId);
            if (dbUser != null)
            {
                var token = await _context.login_tokenek
                    .FirstOrDefaultAsync(x => x.felhasznalo_id == dbUser.id && x.token == jwtToken.RefreshToken);
                if (token != null)
                {
                    _context.login_tokenek.Remove(token);
                    await _context.SaveChangesAsync();
                }
            }

            return NoContent();
        }

        // Követelési szintek létrehozása
        private List<Claim> GetClaimsFromUser(Felhasznalo felhasznalo)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, felhasznalo.id.ToString()),
                new Claim(ClaimTypes.Name, felhasznalo.felhasznalonev),
                new Claim(ClaimTypes.Role, felhasznalo.szerepkor!.nev)
            };
        }
    }
}
