using JWTSecurity.Models;
using JWTSecurity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeendokLista.API.Data;
using TeendokLista.API.Models;

namespace TeendokLista.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TeendokContext _context;
        private readonly JwtManagerService _jwtManager;

        public TokenController(TeendokContext context, JwtManagerService jwtManagerRepository)
        {
            _context = context;
            _jwtManager = jwtManagerRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<JwtToken>> Login(UserLogin userLogin)
        {
            // Felhasználó kikeresése
            var dbFelhasznalo = await _context.felhasznalok.FirstOrDefaultAsync(x =>
                x.felhasznalonev == userLogin.Username);
            // Ha nem létezik a felhasználó
            if (dbFelhasznalo == null)
            {
                return Unauthorized();
            }
            // Jelszó hashelés
            // string password = BCrypt.Net.BCrypt.HashPassword(userLogin.Password);
            // Jelszó ellenőrzése
            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, dbFelhasznalo.jelszo))
            {
                return Unauthorized("Hibás felhasználónév vagy jelszó.");
            }
            // Szerepkör beállítása
            string role = dbFelhasznalo.felhasznalonev == "admin" ? "admin" : "user";
            // Token generálása
            var jwtToken = _jwtManager.GenerateToken(dbFelhasznalo.felhasznalonev, role);
            // Refresh token elmentése az adatbázisba
            dbFelhasznalo.token = jwtToken.RefreshToken;
            dbFelhasznalo.token_lejarat = DateTime.Now.AddDays(7);
            _context.SaveChanges();

            // Token visszaadása
            return jwtToken;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Refresh")]
        public async Task<ActionResult<JwtToken>> Refresh(JwtToken token)
        {
            // Felhasználói adatok kinyerése a tokenből
            var principal = _jwtManager.GetPrincipalFromExpiredToken(token.AccessToken);
            var username = principal.Identity?.Name;
            var dbFelhasznalo = await _context.felhasznalok.FirstOrDefaultAsync(x => x.felhasznalonev == username);
            if (dbFelhasznalo == null)
            {
                return Unauthorized();
            }
            // Ha nem egyezik a refresh token vagy már lejárt
            if (dbFelhasznalo.token != token.RefreshToken || dbFelhasznalo.token_lejarat <= DateTime.Now)
            {
                return Unauthorized();
            }
            string role = dbFelhasznalo.felhasznalonev == "admin" ? "admin" : "user";
            var jwtToken = _jwtManager.GenerateToken(username, role);

            // Refresh token elmentése az adatbázisba
            dbFelhasznalo.token = jwtToken.RefreshToken;
            dbFelhasznalo.token_lejarat = DateTime.Now.AddDays(7);
            _context.SaveChanges();

            return jwtToken;
        }


        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity.Name;
            var dbFelhasznalo = await _context.felhasznalok.FirstOrDefaultAsync(x =>
                x.felhasznalonev == username);
            if (dbFelhasznalo != null)
            {
                dbFelhasznalo.token = null;
                dbFelhasznalo.token_lejarat = null;
                _context.SaveChanges();
            }
            return NoContent();
        }
    }
}
