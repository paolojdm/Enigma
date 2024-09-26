using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class PP_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerProgress_Users_UserId",
                table: "PlayerProgress");

            migrationBuilder.DropIndex(
                name: "IX_PlayerProgress_UserId",
                table: "PlayerProgress");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlayerProgress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PlayerProgress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProgress_UserId",
                table: "PlayerProgress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerProgress_Users_UserId",
                table: "PlayerProgress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
