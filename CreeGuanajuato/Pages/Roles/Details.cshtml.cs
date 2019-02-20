using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Areas.Identity.Data;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public DetailsModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public CreeGuanajuatoRole CreeGuanajuatoRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreeGuanajuatoRole = await _context.CreeGuanajuatoRole.FirstOrDefaultAsync(m => m.Id == id);

            if (CreeGuanajuatoRole == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
