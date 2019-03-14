using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CreeGuanajuato.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreeGuanajuatoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<CreeGuanajuatoUser> _signInManager;
        private readonly UserManager<CreeGuanajuatoUser> _userManager;

        public AuthController(IConfiguration configuration,
            SignInManager<CreeGuanajuatoUser> signInManager,
            UserManager<CreeGuanajuatoUser> userManager) 
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public class loginUser
        {
            public string username { get; set; }
            public string password { get; set; }

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody]loginUser usuario)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.username, usuario.password, true, lockoutOnFailure: true);

            if (result == null)
                return BadRequest(new { message = "El nombre de usuario o contraseña son incorrectas" });

            if (!result.Succeeded)
                return BadRequest(new { message = "El nombre de usuario o contraseña son incorrectas" });
                

            CreeGuanajuatoUser usr = await _userManager.FindByNameAsync(usuario.username);

            var claims = new[]
            {
                new Claim("UserData", JsonConvert.SerializeObject(usr))
            };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["ApiAuth:Issuer"],
                audience: _configuration["ApiAuth:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddYears(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"])),
                SecurityAlgorithms.HmacSha256)
            );

            return Ok(
                new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                }
            );

        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult LoginToken()
        {
            return Ok();
        }
    }
}
