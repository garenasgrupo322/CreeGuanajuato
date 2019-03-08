using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Areas.Identity.Data;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Usuarios
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public DetailsModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public CreeGuanajuatoUser CreeGuanajuatoUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreeGuanajuatoUser = await _context.CreeGuanajuatoUser.FirstOrDefaultAsync(m => m.Id == id);

            if (CreeGuanajuatoUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
