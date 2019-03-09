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

namespace CreeGuanajuato.Pages.Registros
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string CurrentSort { get; private set; }
        public string CurrentFilter { get; set; }
        public int? id_colonia { get; set; }
        public int? id_necesidad { get; set; }
        public int? id_seccion { get; set; }



        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public IndexModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public PaginatedList<Registro> Registro { get;set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string nombre, int? pageIndex, 
            int? id_colonia, int? id_necesidad, int? id_seccion)
        {
            ViewData["id_colonia"] = new SelectList(_context.Colonia, "id_colonia", "nombre_colonia", id_colonia);
            ViewData["id_necesidad"] = new SelectList(_context.Necesidad, "id_necesidad", "descripcion", id_necesidad);
            ViewData["id_seccion"] = new SelectList(_context.Seccion, "id_seccion", "nombre", id_seccion);

            this.id_colonia = id_colonia;
            this.id_necesidad = id_necesidad;
            this.id_seccion = id_seccion;
            
            CurrentSort = sortOrder;
            

            if (nombre != null)
            {
                pageIndex = 1;
            }
            else
            {
                nombre = currentFilter;
            }

            CurrentFilter = nombre;

            IQueryable<Registro> registroIQ = from s in _context.Registro
                .Include(r => r.Colonia)
                .Include(r => r.Direccion)
                .Include(r => r.Escolaridad)
                .Include(r => r.Estado)
                .Include(r => r.EstadoCivil)
                .Include(r => r.Municipio)
                .Include(r => r.Necesidad)
                .OrderByDescending(i => i.id_registro)
                                              select s;

            if (!String.IsNullOrEmpty(nombre))
            {
                registroIQ = registroIQ.Where(s => s.NormalizedNombre.Contains(nombre));
            }

            if (id_colonia != 0 && id_colonia != null) {
                registroIQ = registroIQ.Where(s => s.id_colonia.Equals(id_colonia));
            }

            if (id_necesidad != 0 && id_necesidad != null) {
                registroIQ = registroIQ.Where(s => s.id_necesidad.Equals(id_necesidad));
            }

            if (id_seccion != 0 && id_seccion != null) {
                registroIQ = registroIQ.Where(s => s.id_seccion.Equals(id_seccion));
            }

            int pageSize = 10;
            Registro = await PaginatedList<Registro>.CreateAsync(
                registroIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
