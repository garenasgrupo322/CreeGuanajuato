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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QRCoder;

namespace CreeGuanajuato.Pages.Usuarios
{
    [Authorize]
    public class CardModel : PageModel
    {
        private readonly UserManager<CreeGuanajuatoUser> _userManager;
        private readonly SignInManager<CreeGuanajuatoUser> _signInManager;
        private readonly AppSettings _appSettings;

        public CardModel(UserManager<CreeGuanajuatoUser> userManager,
            SignInManager<CreeGuanajuatoUser> signInManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [BindProperty]
        public CreeGuanajuatoUser CreeGuanajuatoUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            CreeGuanajuatoUser = await _userManager.FindByIdAsync(id);
            if (CreeGuanajuatoUser == null)
            {
                return NotFound($"Unable to load user with ID '{ id }'.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name.ToString(), CreeGuanajuatoUser.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(360),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            MemoryStream ms = new MemoryStream();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(tokenString, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(2);
            qrCodeImage.Save(ms, ImageFormat.Png);
            ViewData["imageQR"] = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

            return Page();
        }
    }
}
