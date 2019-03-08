using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Colonias
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
        ViewData["id_municipio"] = new SelectList(_context.Municipio, "id_municipio", "nombre_municipio");
            return Page();
        }

        [BindProperty]
        public Colonia Colonia { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Colonia.Add(Colonia);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}