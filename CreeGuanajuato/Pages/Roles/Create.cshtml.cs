using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CreeGuanajuato.Areas.Identity.Data;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Roles
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RoleManager<CreeGuanajuatoRole> _roleManager;

        public CreateModel(RoleManager<CreeGuanajuatoRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreeGuanajuatoRole CreeGuanajuatoRole { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var result = await _roleManager.CreateAsync(CreeGuanajuatoRole);

            if (result.Succeeded)
            {
                return RedirectToPage("./Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            //_context.CreeGuanajuatoRole.Add(CreeGuanajuatoRole);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}