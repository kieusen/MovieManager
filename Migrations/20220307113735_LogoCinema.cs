using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_01.Migrations
{
    public partial class LogoCinema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Cinemas");

            migrationBuilder.AddColumn<string>(
                name: "LogoURL",
                table: "Cinemas",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoURL",
                table: "Cinemas");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Cinemas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
