using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Registros
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public EditModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Registro Registro { get; set; }

        [BindProperty]
        public Direccion Direccion { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Registro = await _context.Registro
                .Include(r => r.Colonia)
                .Include(r => r.Direccion)
                .Include(r => r.Escolaridad)
                .Include(r => r.Estado)
                .Include(r => r.EstadoCivil)
                .Include(r => r.Municipio)
                .Include(r => r.Seccion)
                .Include(r => r.Necesidad).FirstOrDefaultAsync(m => m.id_registro == id);

            Direccion = await _context.Direccion
                .FirstOrDefaultAsync(m => m.id_direccion == Registro.id_direccion);

            if (Registro == null)
            {
                return NotFound();
            }
           ViewData["id_colonia"] = new SelectList(_context.Colonia, "id_colonia", "nombre_colonia");
           ViewData["id_direccion"] = new SelectList(_context.Direccion, "id_direccion", "id_direccion");
           ViewData["id_escolaridad"] = new SelectList(_context.Escolaridad, "id_escolaridad", "nombre");
           ViewData["id_estado"] = new SelectList(_context.Estado, "id_estado", "nombre_estado");
           ViewData["id_estado_civil"] = new SelectList(_context.EstadoCivil, "id_estado_civil", "nombre");
           ViewData["id_municipio"] = new SelectList(_context.Municipio, "id_municipio", "nombre_municipio");
           ViewData["id_necesidad"] = new SelectList(_context.Necesidad, "id_necesidad", "descripcion");
           ViewData["id_seccion"] = new SelectList(_context.Seccion, "id_seccion", "nombre");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Direccion.id_colonia = Registro.id_colonia;
            _context.Attach(Direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(Direccion.id_direccion))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Registro.id_direccion = Direccion.id_direccion;
            _context.Attach(Registro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(Registro.id_registro))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RegistroExists(int id)
        {
            return _context.Registro.Any(e => e.id_registro == id);
        }
    }
}
