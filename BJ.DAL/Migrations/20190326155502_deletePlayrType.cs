using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DataAccess.Migrations
{
    public partial class deletePlayrType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Player",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }
    }
}
