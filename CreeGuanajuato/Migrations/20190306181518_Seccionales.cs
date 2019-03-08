using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreeGuanajuato.Migrations
{
    public partial class Seccionales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_seccion",
                table: "Registro",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seccion",
                columns: table => new
                {
                    id_seccion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seccion", x => x.id_seccion);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registro_id_seccion",
                table: "Registro",
                column: "id_seccion");

            migrationBuilder.AddForeignKey(
                name: "FK_Registro_Seccion_id_seccion",
                table: "Registro",
                column: "id_seccion",
                principalTable: "Seccion",
                principalColumn: "id_seccion",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registro_Seccion_id_seccion",
                table: "Registro");

            migrationBuilder.DropTable(
                name: "Seccion");

            migrationBuilder.DropIndex(
                name: "IX_Registro_id_seccion",
                table: "Registro");

            migrationBuilder.DropColumn(
                name: "id_seccion",
                table: "Registro");
        }
    }
}
