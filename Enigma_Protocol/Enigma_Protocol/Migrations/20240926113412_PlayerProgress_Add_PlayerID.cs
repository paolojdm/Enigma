using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class PlayerProgress_Add_PlayerID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "PlayerProgress");

            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "PlayerProgress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PlayerProgress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProgress_PlayerID",
                table: "PlayerProgress",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProgress_UserId",
                table: "PlayerProgress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerProgress_Users_PlayerID",
                table: "PlayerProgress",
                column: "PlayerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerProgress_Users_UserId",
                table: "PlayerProgress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerProgress_Users_PlayerID",
                table: "PlayerProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerProgress_Users_UserId",
                table: "PlayerProgress");

            migrationBuilder.DropIndex(
                name: "IX_PlayerProgress_PlayerID",
                table: "PlayerProgress");

            migrationBuilder.DropIndex(
                name: "IX_PlayerProgress_UserId",
                table: "PlayerProgress");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "PlayerProgress");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlayerProgress");

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "PlayerProgress",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
