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
    public class ApartmentsController : ControllerBase
    {
        private readonly WebApiContext _context;

        public ApartmentsController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Apartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartments(int? startCount = 1, int? endCount = 25)
        {
          if (_context.Apartments == null)
          {
              return NotFound();
          }

          if(startCount <= 0 || endCount <= 0) {
                return BadRequest(new { msg = "Error startCount or endCount  <= 0", statusCode = HttpStatusCode.BadRequest });
          }

            var result = await _context.Apartments.AsNoTracking().
                 Include(h => h.Home)
                .ThenInclude(h => h.Street)
                .ThenInclude(h => h.Locality)
                .Skip(Math.Abs(startCount.GetValueOrDefault() - 1))
                .Take(Math.Abs(endCount.GetValueOrDefault()))
                .ToListAsync();

            return result;
        }

        // GET: api/Apartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Apartment>> GetApartment(int id)
        {
          if (_context.Apartments == null)
          {
              return NotFound();
          }
          var apartment = await _context.Apartments.AsNoTracking()
                .Include(h => h.Home)
                .ThenInclude(h => h.Street)
                .ThenInclude(h => h.Locality)
                .FirstAsync(x => x.Id == id);

          if (apartment == null)
          {
                return NotFound();
          }

          return apartment;
        }

        // PUT: api/Apartments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return BadRequest();
            }

            _context.Entry(apartment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
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

        // POST: api/Apartments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Apartment>> PostApartment(Apartment apartment)
        {
          if (_context.Apartments == null)
          {
              return Problem("Entity set 'WebApiContext.Apartments'  is null.");
          }
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartment", new { id = apartment.Id }, apartment);
        }

        // DELETE: api/Apartments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            if (_context.Apartments == null)
            {
                return NotFound();
            }
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApartmentExists(int id)
        {
            return (_context.Apartments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
