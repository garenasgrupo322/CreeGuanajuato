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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CreeGuanajuato.Pages.Colonias
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

        public PaginatedList<Colonia> Colonia { get;set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string busqueda, int? pageIndex, int? id_municipio)
        {
            ViewData["id_municipio"] = new SelectList(_context.Municipio, "id_municipio", "nombre_municipio");

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

            IQueryable<Colonia> registroIQ = from s in _context.Colonia
                .Include(c => c.Municipio)
                .OrderByDescending(s => s.id_colonia)
                                               select s;

            if (!String.IsNullOrEmpty(busqueda))
            {
                registroIQ = registroIQ.Where(s => s.nombre_colonia.Contains(busqueda) || s.codigo_postal.Contains(busqueda));
            }

            if (id_municipio != 0 && id_municipio != null) {
                registroIQ = registroIQ.Where(s => s.id_municipio.Equals(id_municipio));
            }

            int pageSize = 10;
            Colonia = await PaginatedList<Colonia>.CreateAsync(
                registroIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
