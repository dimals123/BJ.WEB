using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DataAccess.Migrations
{
    public partial class iniacc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StepsAccounts_Cards_CardId",
                table: "StepsAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_StepsBots_Cards_CardId",
                table: "StepsBots");

            migrationBuilder.DropIndex(
                name: "IX_StepsBots_CardId",
                table: "StepsBots");

            migrationBuilder.DropIndex(
                name: "IX_StepsAccounts_CardId",
                table: "StepsAccounts");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "StepsBots");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "StepsBots");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "StepsAccounts");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "StepsAccounts");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "StepsBots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "StepsAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountBots",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Cards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "StepsBots");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "StepsAccounts");

            migrationBuilder.DropColumn(
                name: "CountBots",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Cards");

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "StepsBots",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "StepsBots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "StepsAccounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "StepsAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StepsBots_CardId",
                table: "StepsBots",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_StepsAccounts_CardId",
                table: "StepsAccounts",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_StepsAccounts_Cards_CardId",
                table: "StepsAccounts",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StepsBots_Cards_CardId",
                table: "StepsBots",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
