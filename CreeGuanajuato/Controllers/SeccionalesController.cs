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
    public class SeccionalesController : Controller
    {
        private readonly CreeGuanajuatoContext _context;

        public SeccionalesController(CreeGuanajuatoContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Seccion> Get()
        {
            return _context.Seccion;
        }
    }
}
