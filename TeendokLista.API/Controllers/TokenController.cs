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
        public async Task<ActionResult<LoginDTO>> Login(UserLogin userLogin)
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
            _context.login_tokenek.Add(new LoginToken(jwtToken.Refresh_Token, dbUser.id));
            _context.SaveChanges();

            // Felhasználói adatok és token visszaadása
            return new LoginDTO(dbUser.id, dbUser.felhasznalonev, dbUser.szerepkor.nev, jwtToken);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Refresh")]
        public async Task<ActionResult<JwtToken>> Refresh(JwtToken jwtToken)
        {
            // Felhasználói adatok kinyerése a tokenből
            var principal = _jwtManagerService.GetPrincipalFromExpiredToken(jwtToken.Access_Token);
            var claimId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int.TryParse(claimId.Value, out int userId);

            var dbUser = await _context.felhasznalok
                .Include(x => x.szerepkor)
                .FirstOrDefaultAsync(x => x.id == userId);
            if (dbUser == null)
            {
                return Unauthorized("A felhasználónév nincs regisztrálva.");
            }
            // Token kikeresése
            var oldToken = await _context.login_tokenek
                .FirstOrDefaultAsync(x => x.felhasznalo_id == dbUser.id && x.token == jwtToken.Refresh_Token);
            if (oldToken == null)
            {
                return BadRequest("Érvénytelen token.");
            }
            // Ha nem egyezik a refresh token vagy már lejárt
            if (oldToken.token != jwtToken.Refresh_Token || oldToken.lejarat_datum <= DateTime.Now)
            {
                return Unauthorized("Lejárt vagy érvénytelen token.");
            }

            // Régi token törlése
            _context.login_tokenek.Remove(oldToken);
            // Új token generálása
            var claims = GetClaimsFromUser(dbUser);
            var newToken = _jwtManagerService.GenerateToken(claims);
            // Refresh token elmentése az adatbázisba
            _context.login_tokenek.Add(new LoginToken(newToken.Refresh_Token, dbUser.id));
            _context.SaveChanges();

            return newToken;
        }


        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(JwtToken jwtToken)
        {
            var claimId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int.TryParse(claimId.Value, out int userId);

            var dbUser = await _context.felhasznalok
                .FirstOrDefaultAsync(x => x.id == userId);
            if (dbUser != null)
            {
                var token = await _context.login_tokenek
                    .FirstOrDefaultAsync(x => x.felhasznalo_id == dbUser.id && x.token == jwtToken.Refresh_Token);
                if (token != null)
                {
                    _context.login_tokenek.Remove(token);
                    _context.SaveChanges();
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
                new Claim(ClaimTypes.Role, felhasznalo.szerepkor.nev)
            };
        }
    }
}
