using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CreeGuanajuato.Helpers;
using Microsoft.AspNetCore.Identity;
using CreeGuanajuato.Areas.Identity.Data;
using System.Threading.Tasks;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreeGuanajuato.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
    
        private readonly AppSettings _appSettings;
        private readonly SignInManager<CreeGuanajuatoUser> _signInManager;
        private readonly UserManager<CreeGuanajuatoUser> _userManager;

        public UsersController(
            SignInManager<CreeGuanajuatoUser> signInManager,
            UserManager<CreeGuanajuatoUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        private Task<CreeGuanajuatoUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]loginUser usuario)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.username, usuario.password, true, lockoutOnFailure: true);

            if (result == null)
                return BadRequest(new { message = "El nombre de usuario o contraseña son incorrectas" });

            if (result.Succeeded)
            {

                CreeGuanajuatoUser usr = await _userManager.FindByNameAsync(usuario.username);
                var user_id = usr?.Id;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name.ToString(), user_id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(360),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var rolesUsuario = await _userManager.GetRolesAsync(usr);
                var roles = string.Join(",", rolesUsuario);

                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    nombre = usr.nombre,
                    apellido_paterno = usr.apellido_paterno,
                    apellido_materno = usr.apellido_materno,
                    rol = roles,
                    token = tokenString
                });
            }
            else
            {
                return BadRequest(new { message = "El nombre de usuario o contraseña son incorrectas" });
            }
        }

        public class loginUser {
            public string username { get; set; }
            public string password { get; set; }

        }

        public List<CustomUser> CreeGuanajuatoUser { get; set; }

        public class CustomUser : CreeGuanajuatoUser
        {
            public string rol { get; set; }
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<CustomUser>> GetUsuarios()
        {
            IList<CreeGuanajuatoUser> userIQ = _userManager.Users.ToList();

            CreeGuanajuatoUser = new List<CustomUser>();
            foreach (CreeGuanajuatoUser item in userIQ)
            {
                CustomUser newItem = new CustomUser();
                newItem.Id = item.Id;
                newItem.nombre = item.nombre;
                newItem.apellido_paterno = item.apellido_paterno;
                newItem.apellido_materno = item.apellido_materno;
                newItem.Email = item.Email;
                var rolesUsuario = await _userManager.GetRolesAsync(item);
                newItem.rol = string.Join(",", rolesUsuario);
                CreeGuanajuatoUser.Add(newItem);
            }


            return CreeGuanajuatoUser;
        }

        // GET: api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarios([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var necesidad = await _userManager.FindByIdAsync(id);

            Usuario = new CustomUser();
            Usuario.nombre = necesidad.nombre;
            Usuario.apellido_materno = necesidad.apellido_materno;
            Usuario.apellido_paterno = necesidad.apellido_paterno;
            Usuario.Email = necesidad.Email;
            var rolesUsuario = await _userManager.GetRolesAsync(necesidad);
            Usuario.rol = string.Join(",", rolesUsuario);

            if (necesidad == null)
            {
                return NotFound();
            }

            return Ok(Usuario);
        }

        public CustomUser Usuario { get; set; }

        [Route("autorizacion")]
        [HttpPost]
        public async Task<IActionResult> AuthenticateToken([FromBody]Input token)
        {
            var user = await _userManager.GetUserAsync(User);
            /*var token = string.Empty;
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            //validationParameters.IssuerSigningKey = key;

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);*/



            return Ok(new
            {
                token = ""
            });
        }

        public class Input
        {
            public string token { get; set; }
        }
    }
}
