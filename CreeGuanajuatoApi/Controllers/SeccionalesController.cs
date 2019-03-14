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
    public class SeccionalesController : Controller
    {
        private readonly CreeGuanajuatoContext _context;

        public SeccionalesController(CreeGuanajuatoContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<Seccion> Get()
        {
            return _context.Seccion;
        }
    }
}
