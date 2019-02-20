using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Areas.Identity.Data;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public IndexModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public IList<CreeGuanajuatoRole> CreeGuanajuatoRole { get;set; }

        public async Task OnGetAsync()
        {
            CreeGuanajuatoRole = await _context.CreeGuanajuatoRole.ToListAsync();
        }
    }
}
