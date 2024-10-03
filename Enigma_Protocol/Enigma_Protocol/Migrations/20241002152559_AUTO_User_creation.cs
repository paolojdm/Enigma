using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class AUTO_User_creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CardCVC", "CardNumber", "CardOwner", "CardType", "CreatedAt", "Email", "ExpirationDate", "IsAdmin", "PasswordHash", "ShippingAddress", "UserName" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new DateTime(2024, 10, 2, 17, 25, 59, 310, DateTimeKind.Local).AddTicks(6885), "testemail2@gmail.com", null, false, "AQAAAAIAAYagAAAAEMhU2fip+YkyWX1Lb9EePrwEBx3DN9pUTDdInCNp1otbhZIilQMpvs4RLsNyoif49w==", null, "Presentazione_Utente1" },
                    { 2, null, null, null, null, new DateTime(2024, 10, 2, 17, 25, 59, 310, DateTimeKind.Local).AddTicks(6930), "testemail3@gmail.com", null, true, "AQAAAAIAAYagAAAAEF6JuNnjowt+kv+JEecKS5+XYbBvS1E0jcz+iqWiv8HkrqSGGmmwa6QAyA1MN7ZmAQ==", null, "Presentazione_Admin1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
