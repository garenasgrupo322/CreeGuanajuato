using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreeGuanajuato.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreeGuanajuatoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<CreeGuanajuatoUser> _userManager;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        public CustomUser Usuario { get; set; }

        public UsersController(UserManager<CreeGuanajuatoUser> userManager,
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            _configuration = configuration;
            _userManager = userManager;
            _env = env;
        }

        public List<CustomUser> CreeGuanajuatoUser { get; set; }

        public class CustomUser : CreeGuanajuatoUser
        {
            public string roles { get; set; }
        }

        public class imageProfiler
        {
            public string image { get; set; }
        }

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
                newItem.roles = string.Join(",", rolesUsuario);
                CreeGuanajuatoUser.Add(newItem);
            }


            return CreeGuanajuatoUser;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<CustomUser> Perfil()
        {
            string userjson = User.Claims.FirstOrDefault(c => c.Type == "UserData").Value;

            CreeGuanajuatoUser usuario = JsonConvert.DeserializeObject<CreeGuanajuatoUser>(userjson);
            CreeGuanajuatoUser user = await _userManager.FindByIdAsync(usuario.Id);

            CustomUser newItem = new CustomUser();
            newItem.Id = user.Id;
            newItem.nombre = user.nombre;
            newItem.apellido_paterno = user.apellido_paterno;
            newItem.apellido_materno = user.apellido_materno;
            newItem.Email = user.Email;
            newItem.url = _configuration["urlBase"] + "/images/profilers/" + user.url;
            var rolesUsuario = await _userManager.GetRolesAsync(user);
            newItem.roles = string.Join(",", rolesUsuario);

            return newItem;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult setImage(string post)
        {
            byte[] imageBytes = Convert.FromBase64String(post);
            MemoryStream ms = new MemoryStream(imageBytes, 0,imageBytes.Length);
            
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

            string userjson = User.Claims.FirstOrDefault(c => c.Type == "UserData").Value;

            CreeGuanajuatoUser usuario = JsonConvert.DeserializeObject<CreeGuanajuatoUser>(userjson);

            string nameImage = usuario.Id + DateTime.Now.Ticks.ToString() + "_imageProfiler";

            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, nameImage);

            image.Save(file, System.Drawing.Imaging.ImageFormat.Png);

            return Ok();
        }
    }
}
