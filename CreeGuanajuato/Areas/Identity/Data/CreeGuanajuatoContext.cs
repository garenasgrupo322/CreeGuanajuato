using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreeGuanajuato.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Models
{
    public class CreeGuanajuatoContext : IdentityDbContext<CreeGuanajuatoUser>
    {
        public CreeGuanajuatoContext(DbContextOptions<CreeGuanajuatoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Necesidad> Necesidad { get; set; }

        public DbSet<Escolaridad> Escolaridad { get; set; }

        public DbSet<EstadoCivil> EstadoCivil { get; set; }

        public DbSet<Estado> Estado { get; set; }

        public DbSet<Municipio> Municipio { get; set; }

        public DbSet<Colonia> Colonia { get; set; }

        public DbSet<Direccion> Direccion { get; set; }

        public DbSet<CreeGuanajuatoRole> CreeGuanajuatoRole { get; set; }

        public DbSet<CreeGuanajuato.Models.Registro> Registro { get; set; }

        public DbSet<CreeGuanajuato.Areas.Identity.Data.CreeGuanajuatoUser> CreeGuanajuatoUser { get; set; }

        public DbSet<CreeGuanajuato.Models.Seccion> Seccion { get; set; }

        public DbSet<CreeGuanajuato.Models.Legal> Legal { get; set; }
    }
}
