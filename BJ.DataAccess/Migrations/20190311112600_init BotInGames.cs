using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DataAccess.Migrations
{
    public partial class initBotInGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsBots_Bots_BotId",
                table: "PointsBots");

            migrationBuilder.DropForeignKey(
                name: "FK_PointsBots_Games_GameId",
                table: "PointsBots");

            migrationBuilder.DropTable(
                name: "PointsAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointsBots",
                table: "PointsBots");

            migrationBuilder.RenameTable(
                name: "PointsBots",
                newName: "BotInGames");

            migrationBuilder.RenameIndex(
                name: "IX_PointsBots_GameId",
                table: "BotInGames",
                newName: "IX_BotInGames_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_PointsBots_BotId",
                table: "BotInGames",
                newName: "IX_BotInGames_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotInGames",
                table: "BotInGames",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserInGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationAt = table.Column<DateTime>(nullable: false),
                    CountPoint = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    GameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInGames_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInGames_GameId",
                table: "UserInGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInGames_UserId",
                table: "UserInGames",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotInGames_Bots_BotId",
                table: "BotInGames",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotInGames_Games_GameId",
                table: "BotInGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotInGames_Bots_BotId",
                table: "BotInGames");

            migrationBuilder.DropForeignKey(
                name: "FK_BotInGames_Games_GameId",
                table: "BotInGames");

            migrationBuilder.DropTable(
                name: "UserInGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotInGames",
                table: "BotInGames");

            migrationBuilder.RenameTable(
                name: "BotInGames",
                newName: "PointsBots");

            migrationBuilder.RenameIndex(
                name: "IX_BotInGames_GameId",
                table: "PointsBots",
                newName: "IX_PointsBots_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_BotInGames_BotId",
                table: "PointsBots",
                newName: "IX_PointsBots_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointsBots",
                table: "PointsBots",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PointsAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CountPoint = table.Column<int>(nullable: false),
                    CreationAt = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsAccounts_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointsAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointsAccounts_GameId",
                table: "PointsAccounts",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsAccounts_UserId",
                table: "PointsAccounts",
                column: "UserId");

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
    }
}
