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
    public class EscolaridadesController : ControllerBase
    {
        private readonly CreeGuanajuatoContext _context;

        public EscolaridadesController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/Escolaridades
        [HttpGet]
        public IEnumerable<Escolaridad> GetEscolaridad()
        {
            return _context.Escolaridad;
        }

        // GET: api/Escolaridades/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEscolaridad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var escolaridad = await _context.Escolaridad.FindAsync(id);

            if (escolaridad == null)
            {
                return NotFound();
            }

            return Ok(escolaridad);
        }

        // PUT: api/Escolaridades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscolaridad([FromRoute] int id, [FromBody] Escolaridad escolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != escolaridad.id_escolaridad)
            {
                return BadRequest();
            }

            _context.Entry(escolaridad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscolaridadExists(id))
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

        // POST: api/Escolaridades
        [HttpPost]
        public async Task<IActionResult> PostEscolaridad([FromBody] Escolaridad escolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Escolaridad.Add(escolaridad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEscolaridad", new { id = escolaridad.id_escolaridad }, escolaridad);
        }

        // DELETE: api/Escolaridades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEscolaridad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var escolaridad = await _context.Escolaridad.FindAsync(id);
            if (escolaridad == null)
            {
                return NotFound();
            }

            _context.Escolaridad.Remove(escolaridad);
            await _context.SaveChangesAsync();

            return Ok(escolaridad);
        }

        private bool EscolaridadExists(int id)
        {
            return _context.Escolaridad.Any(e => e.id_escolaridad == id);
        }
    }
}