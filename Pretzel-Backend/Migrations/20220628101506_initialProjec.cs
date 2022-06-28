using Microsoft.EntityFrameworkCore.Migrations;

namespace Pretzel_Backend.Migrations
{
    public partial class initialProjec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Sliders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Sliders");
        }
    }
}
