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
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using CreeGuanajuato.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Usuarios
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SignInManager<CreeGuanajuatoUser> _signInManager;
        private readonly RoleManager<CreeGuanajuatoRole> _roleManager;
        private readonly UserManager<CreeGuanajuatoUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public CreateModel(UserManager<CreeGuanajuatoUser> userManager,
            SignInManager<CreeGuanajuatoUser> signInManager,
            RoleManager<CreeGuanajuatoRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult OnGet()
        {
            ViewData["id_rol"] = new SelectList(_roleManager.Roles, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public CreeGuanajuatoRole creeGuanajuatoRole { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Por favor ingrese el nombre")]
            [DataType(DataType.Text)]
            [Display(Name = "Nombre")]
            public string nombre { get; set; }

            [Required(ErrorMessage = "Por favor ingrese el apellido paterno")]
            [DataType(DataType.Text)]
            [Display(Name = "Apellido paterno")]
            public string apellido_paterno { get; set; }

            [Required(ErrorMessage ="Por favor ingrese el apellido materno")]
            [DataType(DataType.Text)]
            [Display(Name = "Apellido materno")]
            public string apellido_materno { get; set; }

            [Required(ErrorMessage = "Por favor ingrese el correo")]
            [EmailAddress(ErrorMessage = "El formato del correo no es valido")]
            [Display(Name = "Correo")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Por favor ingrese la contraseña")]
            [StringLength(100, ErrorMessage = "La contraseña debe tener al menos 6 y un máximo de 100 caracteres..", MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "El formato de la contraseña no es correcto")]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirma contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación de contraseña no coinciden.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new CreeGuanajuatoUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    nombre = Input.nombre,
                    apellido_paterno = Input.apellido_paterno,
                    apellido_materno = Input.apellido_materno
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation( "User created a new account with password.");

                    var role = await _roleManager.FindByIdAsync(creeGuanajuatoRole.Id);

                    await _userManager.AddToRoleAsync(user, role.Name);
                    return RedirectToPage("./Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}