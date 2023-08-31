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
    public class StreetsController : ControllerBase
    {
        private readonly WebApiContext _context;

        public StreetsController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Streets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreets(int? startCount = 1, int? endCount = 25)
        {
          if (_context.Streets == null)
          {
              return NotFound();
          }
          if (startCount <= 0 || endCount <= 0)
          {
                return BadRequest(new { msg = "Error startCount or endCount  <= 0", statusCode = HttpStatusCode.BadRequest });
         }

         var result = await _context.Streets.AsNoTracking().
                 Include(h => h.Locality)
                .Skip(Math.Abs(startCount.GetValueOrDefault() - 1))
                .Take(Math.Abs(endCount.GetValueOrDefault()))
                .ToListAsync();

         return result;
 
        }

        // GET: api/Streets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Street>> GetStreet(int id)
        {
          if (_context.Streets == null)
          {
              return NotFound();
          }
             
            var street = await _context.Streets.AsNoTracking().
              Include(h => h.Locality)
             .FirstAsync(x => x.Id == id);

            if (street == null)
            {
                return NotFound();
            }

           

            return street;
        }

        // PUT: api/Streets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStreet(int id, Street street)
        {
            if (id != street.Id)
            {
                return BadRequest();
            }

            _context.Entry(street).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreetExists(id))
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

        // POST: api/Streets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Street>> PostStreet(Street street)
        {
          if (_context.Streets == null)
          {
              return Problem("Entity set 'WebApiContext.Streets'  is null.");
          }
            _context.Streets.Add(street);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStreet", new { id = street.Id }, street);
        }

        // DELETE: api/Streets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreet(int id)
        {
            if (_context.Streets == null)
            {
                return NotFound();
            }
            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }

            _context.Streets.Remove(street);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StreetExists(int id)
        {
            return (_context.Streets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
