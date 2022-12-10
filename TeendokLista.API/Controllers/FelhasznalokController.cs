using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeendokLista.API.Data;
using TeendokLista.API.DTOs;
using TeendokLista.API.Models;

namespace TeendokLista.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FelhasznalokController : ControllerBase
    {
        private readonly TeendokContext _context;

        public FelhasznalokController(TeendokContext context)
        {
            _context = context;
        }

        // GET: api/Felhasznalok
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FelhasznaloDTO>>> Getfelhasznalok()
        {
            var list = await _context.felhasznalok.Include(x => x.szerepkor).ToListAsync();
            return list.ToDTO();
        }

        // GET: api/Felhasznalok/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FelhasznaloDTO>> GetFelhasznalo(int id)
        {
            var felhasznalo = await _context.felhasznalok.FindAsync(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            return felhasznalo.ToDTO();
        }

        // PUT: api/Felhasznalok/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFelhasznalo(int id, Felhasznalo felhasznalo)
        {
            if (id != felhasznalo.id)
            {
                return BadRequest();
            }
            felhasznalo.jelszo = BCrypt.Net.BCrypt.HashPassword(felhasznalo.jelszo);
            _context.Entry(felhasznalo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FelhasznaloExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Felhasznalok
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Felhasznalo>> PostFelhasznalo(Felhasznalo felhasznalo)
        {
            _context.felhasznalok.Add(felhasznalo);
            felhasznalo.jelszo = BCrypt.Net.BCrypt.HashPassword(felhasznalo.jelszo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFelhasznalo", new { id = felhasznalo.id }, felhasznalo);
        }

        // DELETE: api/Felhasznalok/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFelhasznalo(int id)
        {
            var felhasznalo = await _context.felhasznalok.FindAsync(id);
            if (felhasznalo == null)
            {
                return NotFound();
            }

            _context.felhasznalok.Remove(felhasznalo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FelhasznaloExists(int id)
        {
            return _context.felhasznalok.Any(e => e.id == id);
        }
    }
}
