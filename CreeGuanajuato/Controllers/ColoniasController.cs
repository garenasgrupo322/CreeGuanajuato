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
    public class ColoniasController : ControllerBase
    {
        private readonly CreeGuanajuatoContext _context;

        public ColoniasController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/Colonias
        [HttpGet]
        public IEnumerable<Colonia> GetColonia()
        {
            return _context.Colonia;
        }

        // GET: api/Colonias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetColonia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var colonia = await _context.Colonia.FindAsync(id);

            if (colonia == null)
            {
                return NotFound();
            }

            return Ok(colonia);
        }

        // PUT: api/Colonias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColonia([FromRoute] int id, [FromBody] Colonia colonia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != colonia.id_colonia)
            {
                return BadRequest();
            }

            _context.Entry(colonia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColoniaExists(id))
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

        // POST: api/Colonias
        [HttpPost]
        public async Task<IActionResult> PostColonia([FromBody] Colonia colonia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Colonia.Add(colonia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColonia", new { id = colonia.id_colonia }, colonia);
        }

        // DELETE: api/Colonias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColonia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var colonia = await _context.Colonia.FindAsync(id);
            if (colonia == null)
            {
                return NotFound();
            }

            _context.Colonia.Remove(colonia);
            await _context.SaveChangesAsync();

            return Ok(colonia);
        }

        private bool ColoniaExists(int id)
        {
            return _context.Colonia.Any(e => e.id_colonia == id);
        }
    }
}