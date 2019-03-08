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

namespace CreeGuanajuato.Pages.Roles
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<CreeGuanajuatoRole> _roleManager;

        public DeleteModel(RoleManager<CreeGuanajuatoRole> roleManager)
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreeGuanajuatoRole = await _roleManager.FindByIdAsync(id);

            if (CreeGuanajuatoRole != null)
            {
                var result = await _roleManager.DeleteAsync(CreeGuanajuatoRole);
                var rolId = await _roleManager.GetRoleIdAsync(CreeGuanajuatoRole);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred deleteing role with ID '{rolId}'.");
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
