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
    public class MunicipiosController : Controller
    {
        private readonly CreeGuanajuatoContext _context;

        public MunicipiosController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Municipio> GetMunicipio(string id_estado)
        {
            if (!string.IsNullOrEmpty(id_estado) && !id_estado.Equals("0"))
            {
                return _context.Municipio.Where(i => i.id_estado.Equals(int.Parse(id_estado)));
            }
            else
            {
                return _context.Municipio;
            }
        }
    }
}
