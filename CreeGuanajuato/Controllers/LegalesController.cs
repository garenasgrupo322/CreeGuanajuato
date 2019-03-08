using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreeGuanajuato.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreeGuanajuato.Controllers
{
    [Route("api/[controller]")]
    public class LegalesController : Controller
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public LegalesController(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return _context.Legal.FirstOrDefault().descripcion;
        }
    }
}
