using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DataAccess.Migrations
{
    public partial class renme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StepsAccounts");

            migrationBuilder.DropTable(
                name: "StepsBots");

            migrationBuilder.CreateTable(
                name: "BotSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationAt = table.Column<DateTime>(nullable: false),
                    BotId = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    StepNumber = table.Column<int>(nullable: false),
                    Suit = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotSteps_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BotSteps_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    GameId = table.Column<Guid>(nullable: false),
                    StepNumber = table.Column<int>(nullable: false),
                    Suit = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSteps_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSteps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotSteps_BotId",
                table: "BotSteps",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_BotSteps_GameId",
                table: "BotSteps",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSteps_GameId",
                table: "UserSteps",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSteps_UserId",
                table: "UserSteps",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotSteps");

            migrationBuilder.DropTable(
                name: "UserSteps");

            migrationBuilder.CreateTable(
                name: "StepsAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationAt = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    StepNumber = table.Column<int>(nullable: false),
                    Suit = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepsAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepsAccounts_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepsAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StepsBots",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BotId = table.Column<Guid>(nullable: false),
                    CreationAt = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    StepNumber = table.Column<int>(nullable: false),
                    Suit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepsBots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepsBots_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepsBots_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StepsAccounts_GameId",
                table: "StepsAccounts",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_StepsAccounts_UserId",
                table: "StepsAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StepsBots_BotId",
                table: "StepsBots",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_StepsBots_GameId",
                table: "StepsBots",
                column: "GameId");
        }
    }
}
