using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DataAccess.Migrations
{
    public partial class fixStartGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsAccounts_Bots_BotId",
                table: "BotsAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsAccounts_Games_GameId",
                table: "BotsAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotsAccounts",
                table: "BotsAccounts");

            migrationBuilder.RenameTable(
                name: "BotsAccounts",
                newName: "PointsBots");

            migrationBuilder.RenameIndex(
                name: "IX_BotsAccounts_GameId",
                table: "PointsBots",
                newName: "IX_PointsBots_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_BotsAccounts_BotId",
                table: "PointsBots",
                newName: "IX_PointsBots_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointsBots",
                table: "PointsBots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsBots_Bots_BotId",
                table: "PointsBots",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PointsBots_Games_GameId",
                table: "PointsBots",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsBots_Bots_BotId",
                table: "PointsBots");

            migrationBuilder.DropForeignKey(
                name: "FK_PointsBots_Games_GameId",
                table: "PointsBots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointsBots",
                table: "PointsBots");

            migrationBuilder.RenameTable(
                name: "PointsBots",
                newName: "BotsAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_PointsBots_GameId",
                table: "BotsAccounts",
                newName: "IX_BotsAccounts_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_PointsBots_BotId",
                table: "BotsAccounts",
                newName: "IX_BotsAccounts_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotsAccounts",
                table: "BotsAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsAccounts_Bots_BotId",
                table: "BotsAccounts",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotsAccounts_Games_GameId",
                table: "BotsAccounts",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
