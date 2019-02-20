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
    public class EstadoCivilesController : ControllerBase
    {
        private readonly CreeGuanajuatoContext _context;

        public EstadoCivilesController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/EstadoCiviles
        [HttpGet]
        public IEnumerable<EstadoCivil> GetEstadoCivil()
        {
            return _context.EstadoCivil;
        }

        // GET: api/EstadoCiviles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoCivil([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estadoCivil = await _context.EstadoCivil.FindAsync(id);

            if (estadoCivil == null)
            {
                return NotFound();
            }

            return Ok(estadoCivil);
        }

        // PUT: api/EstadoCiviles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoCivil([FromRoute] int id, [FromBody] EstadoCivil estadoCivil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estadoCivil.id_estado_civil)
            {
                return BadRequest();
            }

            _context.Entry(estadoCivil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoCivilExists(id))
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

        // POST: api/EstadoCiviles
        [HttpPost]
        public async Task<IActionResult> PostEstadoCivil([FromBody] EstadoCivil estadoCivil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EstadoCivil.Add(estadoCivil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoCivil", new { id = estadoCivil.id_estado_civil }, estadoCivil);
        }

        // DELETE: api/EstadoCiviles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoCivil([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estadoCivil = await _context.EstadoCivil.FindAsync(id);
            if (estadoCivil == null)
            {
                return NotFound();
            }

            _context.EstadoCivil.Remove(estadoCivil);
            await _context.SaveChangesAsync();

            return Ok(estadoCivil);
        }

        private bool EstadoCivilExists(int id)
        {
            return _context.EstadoCivil.Any(e => e.id_estado_civil == id);
        }
    }
}