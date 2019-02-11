using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Pages.colonias
{
    public class EditModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public EditModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Colonia Colonia { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Colonia = await _context.Colonia
                .Include(c => c.Municipio).FirstOrDefaultAsync(m => m.id_colonia == id);

            if (Colonia == null)
            {
                return NotFound();
            }
           ViewData["id_municipio"] = new SelectList(_context.Municipio, "id_municipio", "nombre_municipio");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Colonia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColoniaExists(Colonia.id_colonia))
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

        private bool ColoniaExists(int id)
        {
            return _context.Colonia.Any(e => e.id_colonia == id);
        }
    }
}
