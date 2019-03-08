using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CreeGuanajuato.Pages.Legales
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public IndexModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Legal legal { get; set; }

        public IActionResult OnGet()
        {
            legal = _context.Legal.FirstOrDefault();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(legal.descripcion))
            {
                legal.descripcion = "";
            }

            if (LegalExists(legal.id_legal)) {
                _context.Attach(legal).State = EntityState.Modified;
            }
            else
            {
                _context.Legal.Add(legal);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool LegalExists(int id)
        {
            return _context.Legal.Any(i => i.id_legal == id);
        }
    }
}
