using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;
using CreeGuanajuato.Utils;

namespace CreeGuanajuato.Pages.Registros
{
    public class IndexModel : PageModel
    {
        public string CurrentSort { get; private set; }
        public string CurrentFilter { get; set; }

        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public IndexModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public PaginatedList<Registro> Registro { get;set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string busqueda, int? pageIndex)
        {
            CurrentSort = sortOrder;
            

            if (busqueda != null)
            {
                pageIndex = 1;
            }
            else
            {
                busqueda = currentFilter;
            }

            CurrentFilter = busqueda;

            IQueryable<Registro> registroIQ = from s in _context.Registro
                .Include(r => r.Colonia)
                .Include(r => r.Direccion)
                .Include(r => r.Escolaridad)
                .Include(r => r.Estado)
                .Include(r => r.EstadoCivil)
                .Include(r => r.Municipio)
                .Include(r => r.Necesidad)
                                              select s;

            if (!String.IsNullOrEmpty(busqueda))
            {
                registroIQ = registroIQ.Where(s => s.nombre.Contains(busqueda) || s.apellido_materno.Contains(busqueda) || s.apellido_paterno.Contains(busqueda));
            }

            int pageSize = 3;
            Registro = await PaginatedList<Registro>.CreateAsync(
                registroIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
