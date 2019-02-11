using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Pages.municipios
{
    public class CreateModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public CreateModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["id_estado"] = new SelectList(_context.Estado, "id_estado", "nombre_estado");
            return Page();
        }

        [BindProperty]
        public Municipio Municipio { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Municipio.Add(Municipio);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}