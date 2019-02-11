using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreeGuanajuato.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escolaridad",
                columns: table => new
                {
                    id_escolaridad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolaridad", x => x.id_escolaridad);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    id_estado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre_estado = table.Column<string>(nullable: false),
                    cve_agee = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.id_estado);
                });

            migrationBuilder.CreateTable(
                name: "EstadoCivil",
                columns: table => new
                {
                    id_estado_civil = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCivil", x => x.id_estado_civil);
                });

            migrationBuilder.CreateTable(
                name: "Necesidad",
                columns: table => new
                {
                    id_necesidad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Necesidad", x => x.id_necesidad);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    id_municipio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_estado = table.Column<int>(nullable: false),
                    nombre_municipio = table.Column<string>(nullable: false),
                    cve_agem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.id_municipio);
                    table.ForeignKey(
                        name: "FK_Municipio_Estado_id_estado",
                        column: x => x.id_estado,
                        principalTable: "Estado",
                        principalColumn: "id_estado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colonia",
                columns: table => new
                {
                    id_colonia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_municipio = table.Column<int>(nullable: false),
                    nombre_colonia = table.Column<string>(nullable: false),
                    codigo_postal = table.Column<string>(nullable: true),
                    cve_loc = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colonia", x => x.id_colonia);
                    table.ForeignKey(
                        name: "FK_Colonia_Municipio_id_municipio",
                        column: x => x.id_municipio,
                        principalTable: "Municipio",
                        principalColumn: "id_municipio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    id_direccion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_colonia = table.Column<int>(nullable: false),
                    calle = table.Column<string>(nullable: true),
                    numero = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.id_direccion);
                    table.ForeignKey(
                        name: "FK_Direccion_Colonia_id_colonia",
                        column: x => x.id_colonia,
                        principalTable: "Colonia",
                        principalColumn: "id_colonia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colonia_id_municipio",
                table: "Colonia",
                column: "id_municipio");

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_id_colonia",
                table: "Direccion",
                column: "id_colonia");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_id_estado",
                table: "Municipio",
                column: "id_estado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Escolaridad");

            migrationBuilder.DropTable(
                name: "EstadoCivil");

            migrationBuilder.DropTable(
                name: "Necesidad");

            migrationBuilder.DropTable(
                name: "Colonia");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}
