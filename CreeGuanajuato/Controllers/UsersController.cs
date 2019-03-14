using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreeGuanajuato.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreeGuanajuato.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<CreeGuanajuatoUser> _userManager;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;

        public UsersController(UserManager<CreeGuanajuatoUser> userManager,
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            _configuration = configuration;
            _userManager = userManager;
            _env = env;
        }

        public class imageProfiler
        {
            public string token { get; set; }
            public string image { get; set; }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> setImage([FromBody] imageProfiler post)
        {
            var tokenS = new JwtSecurityTokenHandler().ReadToken(post.token) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "UserData").Value;
            CreeGuanajuatoUser usr = JsonConvert.DeserializeObject<CreeGuanajuatoUser>(jti);
            CreeGuanajuatoUser usuario = await _userManager.FindByIdAsync(usr.Id);

            byte[] imageBytes = Convert.FromBase64String(post.image);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

            string nameImage = usuario.Id + DateTime.Now.Ticks.ToString() + "_imageProfiler.jpg";

            usuario.url = nameImage;
            await _userManager.UpdateAsync(usuario);

            var webRoot = _env.WebRootPath;
            webRoot = webRoot + "/images/profilers/";
            var file = System.IO.Path.Combine(webRoot, nameImage);

            image.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);

            return Ok();
        }
    }
}
