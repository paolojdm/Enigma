using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class Card_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardCVC",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardOwner",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardCVC",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CardOwner",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Users");
        }
    }
}
