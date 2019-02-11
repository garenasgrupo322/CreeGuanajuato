using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Models
{
    public class CreeGuanajuatoContext : DbContext
    {
        public CreeGuanajuatoContext (DbContextOptions<CreeGuanajuatoContext> options)
            : base(options)
        {
        }

        public DbSet<CreeGuanajuato.Models.Direccion> Direccion { get; set; }

        public DbSet<CreeGuanajuato.Models.Necesidad> Necesidad { get; set; }

        public DbSet<CreeGuanajuato.Models.Estado> Estado { get; set; }

        public DbSet<CreeGuanajuato.Models.Municipio> Municipio { get; set; }

        public DbSet<CreeGuanajuato.Models.Colonia> Colonia { get; set; }

        public DbSet<CreeGuanajuato.Models.Escolaridad> Escolaridad { get; set; }

        public DbSet<CreeGuanajuato.Models.EstadoCivil> EstadoCivil { get; set; }
    }
}
