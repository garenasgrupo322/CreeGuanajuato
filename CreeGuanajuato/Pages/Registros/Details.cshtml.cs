using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Pages.Registros
{
    public class DetailsModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public DetailsModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public Registro Registro { get; set; }

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
                .Include(r => r.Necesidad).FirstOrDefaultAsync(m => m.id_registro == id);

            if (Registro == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
