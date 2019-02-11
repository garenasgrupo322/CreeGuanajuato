﻿using System;
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
    public class MunicipiosController : ControllerBase
    {
        private readonly CreeGuanajuatoContext _context;

        public MunicipiosController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/Municipios
        [HttpGet]
        public IEnumerable<Municipio> GetMunicipio()
        {
            return _context.Municipio;
        }

        // GET: api/Municipios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMunicipio([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var municipio = await _context.Municipio.FindAsync(id);

            if (municipio == null)
            {
                return NotFound();
            }

            return Ok(municipio);
        }

        // PUT: api/Municipios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipio([FromRoute] int id, [FromBody] Municipio municipio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != municipio.id_municipio)
            {
                return BadRequest();
            }

            _context.Entry(municipio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipioExists(id))
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

        // POST: api/Municipios
        [HttpPost]
        public async Task<IActionResult> PostMunicipio([FromBody] Municipio municipio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Municipio.Add(municipio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMunicipio", new { id = municipio.id_municipio }, municipio);
        }

        // DELETE: api/Municipios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipio([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var municipio = await _context.Municipio.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }

            _context.Municipio.Remove(municipio);
            await _context.SaveChangesAsync();

            return Ok(municipio);
        }

        private bool MunicipioExists(int id)
        {
            return _context.Municipio.Any(e => e.id_municipio == id);
        }
    }
}