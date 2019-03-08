using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Areas.Identity.Data;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Roles
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly RoleManager<CreeGuanajuatoRole> _roleManager;

        public EditModel(RoleManager<CreeGuanajuatoRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public CreeGuanajuatoRole CreeGuanajuatoRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreeGuanajuatoRole = await _roleManager.FindByIdAsync(id);

            if (CreeGuanajuatoRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var role = await _roleManager.FindByIdAsync(CreeGuanajuatoRole.Id);

            try
            {
                await _roleManager.SetRoleNameAsync(role, CreeGuanajuatoRole.Name);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
