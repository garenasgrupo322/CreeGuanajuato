using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NecesidadesController : ControllerBase
    {
        private readonly CreeGuanajuatoContext _context;

        public NecesidadesController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/Necesidades
        [HttpGet]
        public IEnumerable<Necesidad> GetNecesidad()
        {
            return _context.Necesidad;
        }

        // GET: api/Necesidades/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNecesidad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var necesidad = await _context.Necesidad.FindAsync(id);

            if (necesidad == null)
            {
                return NotFound();
            }

            return Ok(necesidad);
        }

        // PUT: api/Necesidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNecesidad([FromRoute] int id, [FromBody] Necesidad necesidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != necesidad.id_necesidad)
            {
                return BadRequest();
            }

            _context.Entry(necesidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NecesidadExists(id))
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

        // POST: api/Necesidades
        [HttpPost]
        public async Task<IActionResult> PostNecesidad([FromBody] Necesidad necesidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Necesidad.Add(necesidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNecesidad", new { id = necesidad.id_necesidad }, necesidad);
        }

        // DELETE: api/Necesidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNecesidad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var necesidad = await _context.Necesidad.FindAsync(id);
            if (necesidad == null)
            {
                return NotFound();
            }

            _context.Necesidad.Remove(necesidad);
            await _context.SaveChangesAsync();

            return Ok(necesidad);
        }

        private bool NecesidadExists(int id)
        {
            return _context.Necesidad.Any(e => e.id_necesidad == id);
        }
    }
}