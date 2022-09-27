using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeendokLista.API.Data;
using TeendokLista.API.Models;

namespace TeendokLista.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeladatokController : ControllerBase
    {
        private readonly TeendokContext _context;

        public FeladatokController(TeendokContext context)
        {
            _context = context;
        }

        // GET: api/Feladatok
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feladat>>> Getfeladatok()
        {
          if (_context.feladatok == null)
          {
              return NotFound();
          }
            var result = await _context.feladatok.OrderBy(x => x.hatarido).ToListAsync();
            return result;
        }

        // GET: api/Feladatok/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feladat>> GetFeladat(int id)
        {
          if (_context.feladatok == null)
          {
              return NotFound();
          }
            var feladat = await _context.feladatok.FindAsync(id);

            if (feladat == null)
            {
                return NotFound();
            }

            return feladat;
        }

        // PUT: api/Feladatok/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeladat(int id, Feladat feladat)
        {
            if (id != feladat.id)
            {
                return BadRequest();
            }

            _context.Entry(feladat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeladatExists(id))
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

        // POST: api/Feladatok
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feladat>> PostFeladat(Feladat feladat)
        {
          if (_context.feladatok == null)
          {
              return Problem("Entity set 'TeendokContext.feladatok'  is null.");
          }
            _context.feladatok.Add(feladat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeladat", new { id = feladat.id }, feladat);
        }

        // DELETE: api/Feladatok/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeladat(int id)
        {
            if (_context.feladatok == null)
            {
                return NotFound();
            }
            var feladat = await _context.feladatok.FindAsync(id);
            if (feladat == null)
            {
                return NotFound();
            }

            _context.feladatok.Remove(feladat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeladatExists(int id)
        {
            return (_context.feladatok?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
