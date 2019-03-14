using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreeGuanajuatoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ColoniasController : Controller
    {
        private readonly CreeGuanajuatoContext _context;

        public ColoniasController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Colonia> GetColonia(string id_municipio)
        {
            if (!string.IsNullOrEmpty(id_municipio) && !id_municipio.Equals('0'))
            {
                return _context.Colonia.Where(i => i.id_municipio.Equals(int.Parse(id_municipio)));
            }
            else
            {
                return _context.Colonia;
            }
        }
    }
}
