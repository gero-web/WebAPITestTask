using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITestTask.Models;
using WebAPITestTask.Db;

namespace WebAPITestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly WebApiContext _context;

        public HomesController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Homes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Home>>> GetHomes(int? startCount = 1, int? endCount = 25)
        {
          if (_context.Homes == null)
          {
              return NotFound();
          }

          if (startCount <= 0 || endCount <= 0)
          {
                return BadRequest(new { msg = "Error startCount or endCount  <= 0", statusCode = HttpStatusCode.BadRequest });
          }

            var result = await _context.Homes.AsNoTracking().
                 Include(h => h.Street)
                .ThenInclude(h => h.Locality)
                .Skip(Math.Abs(startCount.GetValueOrDefault() - 1))
                .Take(Math.Abs(endCount.GetValueOrDefault()))
                .OrderBy(h => h.Id)
                .ToListAsync();

            return result;
 
        }

        // GET: api/Homes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Home>> GetHome(int id)
        {
          if (_context.Homes == null)
          {
              return NotFound();
          }
           
            var home = await _context.Homes.AsNoTracking().
                 Include(h => h.Street)
                .ThenInclude(h => h.Locality)
                .FirstAsync(x=> x.Id == id);

            if (home == null)
            {
                return NotFound();
            }

            return home;
        }

        // PUT: api/Homes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHome(int id, Home home)
        {
            if (id != home.Id)
            {
                return BadRequest();
            }

            _context.Entry(home).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeExists(id))
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

        // POST: api/Homes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Home>> PostHome(Home home)
        {
          if (_context.Homes == null)
          {
              return Problem("Entity set 'WebApiContext.Homes'  is null.");
          }
            _context.Homes.Add(home);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHome", new { id = home.Id }, home);
        }

        // DELETE: api/Homes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            if (_context.Homes == null)
            {
                return NotFound();
            }
            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }

            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HomeExists(int id)
        {
            return (_context.Homes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
