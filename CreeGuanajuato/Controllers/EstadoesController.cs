using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;
using CreeGuanajuato.Utilidades;
using static CreeGuanajuato.Utilidades.ThreadsInegi;
using System.Threading;

namespace CreeGuanajuato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoesController : Controller
    {
        private readonly CreeGuanajuatoContext _context;

        public EstadoesController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/Estadoes
        [HttpGet]
        public JsonResult GetEstado()
        {
            return Json(_context.Estado);
        }

        // GET: api/Estadoes/sincroniza
        [HttpGet("{parametro}")]
        public async Task<JsonResult> SetSincronizacion([FromRoute] string parametro)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (parametro.Equals("sincroniza")) {
                ThreadEstado threadEstado = new ThreadEstado(_context);
                await threadEstado.ThreadProc();
            }

            result["success"] = true;

            return Json(result);
        }

        // PUT: api/Estadoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado([FromRoute] int id, [FromBody] Estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estado.id_estado)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
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

        // POST: api/Estadoes
        [HttpPost]
        public async Task<IActionResult> PostEstado([FromBody] Estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Estado.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.id_estado }, estado);
        }


        private bool EstadoExists(int id)
        {
            return _context.Estado.Any(e => e.id_estado == id);
        }
    }
}