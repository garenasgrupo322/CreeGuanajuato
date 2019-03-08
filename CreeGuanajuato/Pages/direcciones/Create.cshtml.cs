using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Direcciones
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public CreateModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["id_colonia"] = new SelectList(_context.Colonia, "id_colonia", "nombre_colonia");
            return Page();
        }

        [BindProperty]
        public Direccion Direccion { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Direccion.Add(Direccion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}