using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CreeGuanajuato.Areas.Identity.Data;
using CreeGuanajuato.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QRCoder;

namespace CreeGuanajuato.Pages.Usuarios
{
    [Authorize]
    public class CardModel : PageModel
    {
        private readonly UserManager<CreeGuanajuatoUser> _userManager;
        private readonly SignInManager<CreeGuanajuatoUser> _signInManager;
        private readonly IConfiguration _configuration;

        public CardModel(UserManager<CreeGuanajuatoUser> userManager,
            SignInManager<CreeGuanajuatoUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [BindProperty]
        public CreeGuanajuatoUser CreeGuanajuatoUser { get; set; }

        public string roles { get; set; }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            CreeGuanajuatoUser = await _userManager.FindByIdAsync(id);
            CreeGuanajuatoUser usr = await _userManager.FindByNameAsync(CreeGuanajuatoUser.UserName);

            if (CreeGuanajuatoUser == null)
            {
                return NotFound($"Unable to load user with ID '{ id }'.");
            }

            if (string.IsNullOrEmpty(usr.url))
            {
                usr.url = "/images/logo_color.png";
            }
            else
            {
                usr.url = "/images/profiler/" + usr.url;
            }

            var rolesUsuario = await _userManager.GetRolesAsync(usr);
            roles = string.Join(",", rolesUsuario);

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

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            MemoryStream ms = new MemoryStream();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(tokenString, QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(2);
            qrCodeImage.Save(ms, ImageFormat.Png);
            ViewData["imageQR"] = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

            return Page();
        }
    }
}
