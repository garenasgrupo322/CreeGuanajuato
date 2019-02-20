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
    public class DireccionesController : ControllerBase
    {
        private readonly CreeGuanajuatoContext _context;

        public DireccionesController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/Direcciones
        [HttpGet]
        public IEnumerable<Direccion> GetDireccion()
        {
            return _context.Direccion;
        }

        // GET: api/Direcciones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDireccion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var direccion = await _context.Direccion.FindAsync(id);

            if (direccion == null)
            {
                return NotFound();
            }

            return Ok(direccion);
        }

        // PUT: api/Direcciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccion([FromRoute] int id, [FromBody] Direccion direccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != direccion.id_direccion)
            {
                return BadRequest();
            }

            _context.Entry(direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionExists(id))
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

        // POST: api/Direcciones
        [HttpPost]
        public async Task<IActionResult> PostDireccion([FromBody] Direccion direccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDireccion", new { id = direccion.id_direccion }, direccion);
        }

        // DELETE: api/Direcciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var direccion = await _context.Direccion.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }

            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();

            return Ok(direccion);
        }

        private bool DireccionExists(int id)
        {
            return _context.Direccion.Any(e => e.id_direccion == id);
        }
    }
}