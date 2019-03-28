using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class renameViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountStep",
                table: "StepsBots",
                newName: "StepNumber");

            migrationBuilder.RenameColumn(
                name: "CountStep",
                table: "StepsAccounts",
                newName: "StepNumber");

            migrationBuilder.RenameColumn(
                name: "IsEnd",
                table: "Games",
                newName: "IsFinished");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StepNumber",
                table: "StepsBots",
                newName: "CountStep");

            migrationBuilder.RenameColumn(
                name: "StepNumber",
                table: "StepsAccounts",
                newName: "CountStep");

            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "Games",
                newName: "IsEnd");
        }
    }
}
