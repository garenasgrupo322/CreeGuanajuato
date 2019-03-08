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
using Microsoft.Extensions.Logging;
using CreeGuanajuato.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Usuarios
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<CreeGuanajuatoUser> _userManager;
        private readonly SignInManager<CreeGuanajuatoUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        public DeleteModel(UserManager<CreeGuanajuatoUser> userManager,
            SignInManager<CreeGuanajuatoUser> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public CreeGuanajuatoUser CreeGuanajuatoUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreeGuanajuatoUser = await _userManager.FindByIdAsync(id);

            if (CreeGuanajuatoUser == null)
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

            CreeGuanajuatoUser = await _userManager.FindByIdAsync(id);

            if (CreeGuanajuatoUser != null)
            {
                var result = await _userManager.DeleteAsync(CreeGuanajuatoUser);
                var userId = await _userManager.GetUserIdAsync(CreeGuanajuatoUser);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
                }

                _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);
            }

            return RedirectToPage("./Index");
        }
    }
}
