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
            var dbUser = await _context.felhasznalok
                .Include(x => x.szerepkor)
                .FirstOrDefaultAsync(x => x.felhasznalonev == userLogin.Username);
            // Ha nem létezik a felhasználó
            if (dbUser == null)
            {
                return Unauthorized();
            }
            // Jelszó hashelés
            // string password = BCrypt.Net.BCrypt.HashPassword(userLogin.Password);
            // Jelszó ellenőrzése
            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, dbUser.jelszo))
            {
                return Unauthorized("Hibás felhasználónév vagy jelszó.");
            }

            // Új Token generálása
            var jwtToken = _jwtManager.GenerateToken(dbUser.felhasznalonev, dbUser.szerepkor.nev);
            // Refresh token elmentése az adatbázisba
            _context.tokenek.Add(new Token(jwtToken.Refresh_Token, dbUser.id));
            _context.SaveChanges();

            // Token visszaadása
            return jwtToken;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Refresh")]
        public async Task<ActionResult<JwtToken>> Refresh(JwtToken jwtToken)
        {
            // Felhasználói adatok kinyerése a tokenből
            var principal = _jwtManager.GetPrincipalFromExpiredToken(jwtToken.Access_Token);
            var username = principal.Identity.Name;
            var dbUser = await _context.felhasznalok
                .Include(x => x.szerepkor)
                .FirstOrDefaultAsync(x => x.felhasznalonev == username );
            if (dbUser == null)
            {
                return Unauthorized();
            }
            // Token kikeresése
            var refreshToken = await _context.tokenek
                .FirstOrDefaultAsync(x => x.felhasznalo_id == dbUser.id && x.token == jwtToken.Refresh_Token);
            // Ha nem egyezik a refresh token vagy már lejárt
            if (refreshToken.token != jwtToken.Refresh_Token || refreshToken.lejarat_datum <= DateTime.Now)
            {
                return Unauthorized();
            }

            // Új token generálása
            var newToken = _jwtManager.GenerateToken(username, dbUser.szerepkor.nev);
            // Refresh token elmentése az adatbázisba
            _context.tokenek.Add(new Token(newToken.Refresh_Token, dbUser.id));
            _context.SaveChanges();

            return newToken;
        }


        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(JwtToken jwtToken)
        {
            var username = User.Identity.Name;
            var dbUser = await _context.felhasznalok
                .FirstOrDefaultAsync(x => x.felhasznalonev == username);
            if (dbUser != null)
            {
                var token = await _context.tokenek
                    .FirstOrDefaultAsync(x => x.felhasznalo_id == dbUser.id && x.token == jwtToken.Refresh_Token);
                if (token != null)
                {
                    _context.tokenek.Remove(token);
                    _context.SaveChanges();
                }
            }
            return NoContent();
        }
    }
}
