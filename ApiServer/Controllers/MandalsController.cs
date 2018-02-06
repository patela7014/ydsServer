using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Core.Models;
using DAL.Persistence;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Mandals")]
    public class MandalsController : Controller
    {
        private readonly ApiContext _context;

        public MandalsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Mandals
        [HttpGet]
        public IEnumerable<Mandal> GetMandal()
        {
            return _context.Mandal;
        }

        // GET: api/Mandals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMandal([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mandal = await _context.Mandal.SingleOrDefaultAsync(m => m.Id == id);

            if (mandal == null)
            {
                return NotFound();
            }

            return Ok(mandal);
        }

        // PUT: api/Mandals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMandal([FromRoute] string id, [FromBody] Mandal mandal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mandal.Id)
            {
                return BadRequest();
            }

            _context.Entry(mandal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MandalExists(id))
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

        // POST: api/Mandals
        [HttpPost]
        public async Task<IActionResult> PostMandal([FromBody] Mandal mandal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Mandal.Add(mandal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMandal", new { id = mandal.Id }, mandal);
        }

        // DELETE: api/Mandals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMandal([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mandal = await _context.Mandal.SingleOrDefaultAsync(m => m.Id == id);
            if (mandal == null)
            {
                return NotFound();
            }

            _context.Mandal.Remove(mandal);
            await _context.SaveChangesAsync();

            return Ok(mandal);
        }

        private bool MandalExists(string id)
        {
            return _context.Mandal.Any(e => e.Id == id);
        }
    }
}