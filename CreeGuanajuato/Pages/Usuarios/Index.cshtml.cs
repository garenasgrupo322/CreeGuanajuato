using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Areas.Identity.Data;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Usuarios
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public readonly UserManager<CreeGuanajuatoUser> _userManager;
        public string CurrentSort { get; private set; }
        public string CurrentFilter { get; set; }

        public IndexModel(UserManager<CreeGuanajuatoUser> userManager)
        {
            _userManager = userManager;
        }

        public List<CustomUser> CreeGuanajuatoUser { get;set; }

        public class CustomUser : CreeGuanajuatoUser {
            public string roles { get; set; }
        }

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

            IQueryable<CreeGuanajuatoUser> registroIQ = from s in _userManager.Users
                .OrderByDescending(i => i.Id)
                                              select s;

            if (!String.IsNullOrEmpty(busqueda))
            {
                registroIQ = registroIQ.Where(s => s.nombre.Contains(busqueda) || s.apellido_paterno.Contains(busqueda) || s.apellido_materno.Contains(busqueda) || s.Email.Contains(busqueda));
            }

            /*IList<CreeGuanajuatoUser> userIQ = (from s in _userManager.Users
                                                    select s).ToList();
                                                    */
            CreeGuanajuatoUser = new List<CustomUser>();
            foreach (CreeGuanajuatoUser item in registroIQ)
            {
                CustomUser newItem = new CustomUser();
                newItem.Id = item.Id;
                newItem.nombre = item.nombre;
                newItem.apellido_paterno = item.apellido_paterno;
                newItem.apellido_materno = item.apellido_materno;
                newItem.Email = item.Email;
                var rolesUsuario = await _userManager.GetRolesAsync(item);
                newItem.roles = string.Join(",", rolesUsuario);
                CreeGuanajuatoUser.Add(newItem);
            }
        }
    }
}
