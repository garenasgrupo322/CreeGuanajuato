using Microsoft.EntityFrameworkCore.Migrations;

namespace CreeGuanajuato.Migrations
{
    public partial class RegistroCoordenadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "latitud",
                table: "Registro",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "longitud",
                table: "Registro",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitud",
                table: "Registro");

            migrationBuilder.DropColumn(
                name: "longitud",
                table: "Registro");
        }
    }
}
