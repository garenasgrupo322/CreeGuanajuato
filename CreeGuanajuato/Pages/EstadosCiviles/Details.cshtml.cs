using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.EstadosCiviles
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public DetailsModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public EstadoCivil EstadoCivil { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstadoCivil = await _context.EstadoCivil.FirstOrDefaultAsync(m => m.id_estado_civil == id);

            if (EstadoCivil == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
