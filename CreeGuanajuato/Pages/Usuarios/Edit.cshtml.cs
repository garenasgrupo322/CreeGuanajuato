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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace CreeGuanajuato.Pages.Usuarios
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly UserManager<CreeGuanajuatoUser> _userManager;
        private readonly RoleManager<CreeGuanajuatoRole> _roleManager;
        private readonly SignInManager<CreeGuanajuatoUser> _signInManager;

        public EditModel(UserManager<CreeGuanajuatoUser> userManager,
            RoleManager<CreeGuanajuatoRole> roleManager,
            SignInManager<CreeGuanajuatoUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [BindProperty]
        public CreeGuanajuatoUser creeGuanajuatoUser { get; set; }

        [BindProperty]
        public CreeGuanajuatoRole creeGuanajuatoRole { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

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

            [Required(ErrorMessage = "Por favor ingrese el apellido materno")]
            [DataType(DataType.Text)]
            [Display(Name = "Apellido materno")]
            public string apellido_materno { get; set; }

            [Required(ErrorMessage = "Por favor ingrese el correo")]
            [EmailAddress(ErrorMessage = "El formato del correo no es valido")]
            [Display(Name = "Correo")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            creeGuanajuatoUser = await _userManager.FindByIdAsync(id);

            ViewData["id_rol"] = new SelectList(_roleManager.Roles, "Id", "Name");

            var rolesUsuario = await _userManager.GetRolesAsync(creeGuanajuatoUser);
            string nameRolUsuario = rolesUsuario.First();
            creeGuanajuatoRole = await _roleManager.FindByNameAsync(nameRolUsuario);

            if (creeGuanajuatoUser == null)
            {
                return NotFound();
            }

            var userName = await _userManager.GetUserNameAsync(creeGuanajuatoUser);
            var email = await _userManager.GetEmailAsync(creeGuanajuatoUser);

            Username = userName;

            Input = new InputModel
            {
                nombre = creeGuanajuatoUser.nombre,
                apellido_paterno = creeGuanajuatoUser.apellido_paterno,
                apellido_materno = creeGuanajuatoUser.apellido_materno,
                Email = email
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(creeGuanajuatoUser.Id);

            if (user == null)
            {
                return Page();
            }

            if (Input.nombre != user.nombre)
            {
                user.nombre = Input.nombre;
            }

            if (Input.apellido_paterno != user.apellido_paterno)
            {
                user.apellido_paterno = Input.apellido_paterno;
            }

            if (Input.apellido_materno != user.apellido_materno)
            {
                user.apellido_materno = Input.apellido_materno;
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);

            return RedirectToPage("./Index");
        }
    }
}
