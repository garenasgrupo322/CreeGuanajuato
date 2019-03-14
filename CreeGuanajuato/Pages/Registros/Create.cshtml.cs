using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CreeGuanajuato.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Registros
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
            ViewData["id_escolaridad"] = new SelectList(_context.Escolaridad, "id_escolaridad", "nombre");
            ViewData["id_estado"] = new SelectList(_context.Estado, "id_estado", "nombre_estado");
            ViewData["id_estado_civil"] = new SelectList(_context.EstadoCivil, "id_estado_civil", "nombre");
            ViewData["id_necesidad"] = new SelectList(_context.Necesidad, "id_necesidad", "descripcion");
            ViewData["id_seccion"] = new SelectList(_context.Seccion, "id_seccion", "nombre");
            return Page();
        }

        [BindProperty]
        public CustomRegistro Registro { get; set; }

        public class CustomRegistro : Registro {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Calle")]
            public string calle { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Número")]
            public string numero { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Direccion direccion = new Direccion();
            direccion.calle = Registro.calle;
            direccion.numero = Registro.numero;
            direccion.id_colonia = Registro.id_colonia;

            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();

            Registro.id_direccion = direccion.id_direccion;

            Registro.NormalizedNombre = Registro.nombre + " " + Registro.apellido_paterno + " " + Registro.apellido_materno;
            _context.Registro.Add(Registro);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }   
}