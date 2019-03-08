using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;
using CreeGuanajuato.Utils;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Municipios
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;
        public string CurrentSort { get; private set; }
        public string CurrentFilter { get; set; }

        public IndexModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public PaginatedList<Municipio> Municipio { get;set; }

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

            IQueryable<Municipio> registroIQ = from s in _context.Municipio
                .Include(m => m.Estado)
                .OrderByDescending(i => i.id_municipio)
                                              select s;
            if (!String.IsNullOrEmpty(busqueda))
            {
                registroIQ = registroIQ.Where(s => s.nombre_municipio.Contains(busqueda));
            }

            int pageSize = 10;
            Municipio = await PaginatedList<Municipio>.CreateAsync(
                registroIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
