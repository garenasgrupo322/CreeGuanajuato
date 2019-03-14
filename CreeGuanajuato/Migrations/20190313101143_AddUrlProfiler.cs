using Microsoft.EntityFrameworkCore.Migrations;

namespace CreeGuanajuato.Migrations
{
    public partial class AddUrlProfiler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url",
                table: "AspNetUsers");
        }
    }
}
