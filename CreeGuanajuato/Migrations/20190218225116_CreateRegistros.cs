using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreeGuanajuato.Migrations
{
    public partial class CreateRegistros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Registro",
                columns: table => new
                {
                    id_registro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_estado = table.Column<int>(nullable: true),
                    id_municipio = table.Column<int>(nullable: true),
                    id_colonia = table.Column<int>(nullable: true),
                    id_direccion = table.Column<int>(nullable: true),
                    id_escolaridad = table.Column<int>(nullable: true),
                    id_estado_civil = table.Column<int>(nullable: true),
                    id_necesidad = table.Column<int>(nullable: true),
                    nombre = table.Column<string>(nullable: false),
                    apellido_paterno = table.Column<string>(nullable: false),
                    apellido_materno = table.Column<string>(nullable: false),
                    INE = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(nullable: false),
                    numero_hijos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registro", x => x.id_registro);
                    table.ForeignKey(
                        name: "FK_Registro_Colonia_id_colonia",
                        column: x => x.id_colonia,
                        principalTable: "Colonia",
                        principalColumn: "id_colonia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registro_Direccion_id_direccion",
                        column: x => x.id_direccion,
                        principalTable: "Direccion",
                        principalColumn: "id_direccion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registro_Escolaridad_id_escolaridad",
                        column: x => x.id_escolaridad,
                        principalTable: "Escolaridad",
                        principalColumn: "id_escolaridad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registro_Estado_id_estado",
                        column: x => x.id_estado,
                        principalTable: "Estado",
                        principalColumn: "id_estado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registro_EstadoCivil_id_estado_civil",
                        column: x => x.id_estado_civil,
                        principalTable: "EstadoCivil",
                        principalColumn: "id_estado_civil",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registro_Municipio_id_municipio",
                        column: x => x.id_municipio,
                        principalTable: "Municipio",
                        principalColumn: "id_municipio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registro_Necesidad_id_necesidad",
                        column: x => x.id_necesidad,
                        principalTable: "Necesidad",
                        principalColumn: "id_necesidad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_colonia",
                table: "Registro",
                column: "id_colonia");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_direccion",
                table: "Registro",
                column: "id_direccion");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_escolaridad",
                table: "Registro",
                column: "id_escolaridad");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_estado",
                table: "Registro",
                column: "id_estado");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_estado_civil",
                table: "Registro",
                column: "id_estado_civil");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_municipio",
                table: "Registro",
                column: "id_municipio");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_necesidad",
                table: "Registro",
                column: "id_necesidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registro");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
