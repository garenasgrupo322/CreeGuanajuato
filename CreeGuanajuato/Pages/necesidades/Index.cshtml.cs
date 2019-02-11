﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Pages.necesidades
{
    public class IndexModel : PageModel
    {
        private readonly CreeGuanajuato.Models.CreeGuanajuatoContext _context;

        public IndexModel(CreeGuanajuato.Models.CreeGuanajuatoContext context)
        {
            _context = context;
        }

        public IList<Necesidad> Necesidad { get;set; }

        public async Task OnGetAsync()
        {
            Necesidad = await _context.Necesidad.ToListAsync();
        }
    }
}
