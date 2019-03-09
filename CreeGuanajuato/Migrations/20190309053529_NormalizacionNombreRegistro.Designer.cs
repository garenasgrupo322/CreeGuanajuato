﻿// <auto-generated />
using System;
using CreeGuanajuato.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CreeGuanajuato.Migrations
{
    [DbContext(typeof(CreeGuanajuatoContext))]
    [Migration("20190309053529_NormalizacionNombreRegistro")]
    partial class NormalizacionNombreRegistro
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CreeGuanajuato.Areas.Identity.Data.CreeGuanajuatoUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("apellido_materno");

                    b.Property<string>("apellido_paterno");

                    b.Property<string>("nombre");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Colonia", b =>
                {
                    b.Property<int>("id_colonia")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("codigo_postal");

                    b.Property<string>("cve_loc");

                    b.Property<int?>("id_municipio")
                        .IsRequired();

                    b.Property<string>("nombre_colonia")
                        .IsRequired();

                    b.HasKey("id_colonia");

                    b.HasIndex("id_municipio");

                    b.ToTable("Colonia");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Direccion", b =>
                {
                    b.Property<int>("id_direccion")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("calle");

                    b.Property<int?>("id_colonia")
                        .IsRequired();

                    b.Property<string>("numero");

                    b.HasKey("id_direccion");

                    b.HasIndex("id_colonia");

                    b.ToTable("Direccion");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Escolaridad", b =>
                {
                    b.Property<int>("id_escolaridad")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nombre")
                        .IsRequired();

                    b.HasKey("id_escolaridad");

                    b.ToTable("Escolaridad");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Estado", b =>
                {
                    b.Property<int>("id_estado")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("cve_agee");

                    b.Property<string>("nombre_estado")
                        .IsRequired();

                    b.HasKey("id_estado");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.EstadoCivil", b =>
                {
                    b.Property<int>("id_estado_civil")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nombre")
                        .IsRequired();

                    b.HasKey("id_estado_civil");

                    b.ToTable("EstadoCivil");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Legal", b =>
                {
                    b.Property<int>("id_legal")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("descripcion")
                        .IsRequired();

                    b.HasKey("id_legal");

                    b.ToTable("Legal");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Municipio", b =>
                {
                    b.Property<int>("id_municipio")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("cve_agem");

                    b.Property<int?>("id_estado")
                        .IsRequired();

                    b.Property<string>("nombre_municipio")
                        .IsRequired();

                    b.HasKey("id_municipio");

                    b.HasIndex("id_estado");

                    b.ToTable("Municipio");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Necesidad", b =>
                {
                    b.Property<int>("id_necesidad")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("descripcion")
                        .IsRequired();

                    b.HasKey("id_necesidad");

                    b.ToTable("Necesidad");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Registro", b =>
                {
                    b.Property<int>("id_registro")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("INE")
                        .IsRequired();

                    b.Property<string>("NormalizedNombre");

                    b.Property<string>("apellido_materno")
                        .IsRequired();

                    b.Property<string>("apellido_paterno")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<DateTime>("fecha_nacimiento");

                    b.Property<int?>("id_colonia");

                    b.Property<int?>("id_direccion");

                    b.Property<int?>("id_escolaridad");

                    b.Property<int?>("id_estado");

                    b.Property<int?>("id_estado_civil");

                    b.Property<int?>("id_municipio");

                    b.Property<int?>("id_necesidad");

                    b.Property<int?>("id_seccion");

                    b.Property<double>("latitud");

                    b.Property<double>("longitud");

                    b.Property<string>("nombre")
                        .IsRequired();

                    b.Property<int>("numero_hijos");

                    b.HasKey("id_registro");

                    b.HasIndex("id_colonia");

                    b.HasIndex("id_direccion");

                    b.HasIndex("id_escolaridad");

                    b.HasIndex("id_estado");

                    b.HasIndex("id_estado_civil");

                    b.HasIndex("id_municipio");

                    b.HasIndex("id_necesidad");

                    b.HasIndex("id_seccion");

                    b.ToTable("Registro");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Seccion", b =>
                {
                    b.Property<int>("id_seccion")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nombre")
                        .IsRequired();

                    b.HasKey("id_seccion");

                    b.ToTable("Seccion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CreeGuanajuato.Areas.Identity.Data.CreeGuanajuatoRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");


                    b.ToTable("CreeGuanajuatoRole");

                    b.HasDiscriminator().HasValue("CreeGuanajuatoRole");
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Colonia", b =>
                {
                    b.HasOne("CreeGuanajuato.Models.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("id_municipio")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Direccion", b =>
                {
                    b.HasOne("CreeGuanajuato.Models.Colonia", "Colonia")
                        .WithMany()
                        .HasForeignKey("id_colonia")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Municipio", b =>
                {
                    b.HasOne("CreeGuanajuato.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("id_estado")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CreeGuanajuato.Models.Registro", b =>
                {
                    b.HasOne("CreeGuanajuato.Models.Colonia", "Colonia")
                        .WithMany()
                        .HasForeignKey("id_colonia");

                    b.HasOne("CreeGuanajuato.Models.Direccion", "Direccion")
                        .WithMany()
                        .HasForeignKey("id_direccion");

                    b.HasOne("CreeGuanajuato.Models.Escolaridad", "Escolaridad")
                        .WithMany()
                        .HasForeignKey("id_escolaridad");

                    b.HasOne("CreeGuanajuato.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("id_estado");

                    b.HasOne("CreeGuanajuato.Models.EstadoCivil", "EstadoCivil")
                        .WithMany()
                        .HasForeignKey("id_estado_civil");

                    b.HasOne("CreeGuanajuato.Models.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("id_municipio");

                    b.HasOne("CreeGuanajuato.Models.Necesidad", "Necesidad")
                        .WithMany()
                        .HasForeignKey("id_necesidad");

                    b.HasOne("CreeGuanajuato.Models.Seccion", "Seccion")
                        .WithMany()
                        .HasForeignKey("id_seccion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CreeGuanajuato.Areas.Identity.Data.CreeGuanajuatoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CreeGuanajuato.Areas.Identity.Data.CreeGuanajuatoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CreeGuanajuato.Areas.Identity.Data.CreeGuanajuatoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CreeGuanajuato.Areas.Identity.Data.CreeGuanajuatoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
