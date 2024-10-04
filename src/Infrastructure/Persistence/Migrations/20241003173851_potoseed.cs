using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class potoseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 10, 3, 17, 38, 49, 811, DateTimeKind.Utc).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 10, 3, 17, 38, 49, 811, DateTimeKind.Utc).AddTicks(9907));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 10, 3, 17, 38, 49, 811, DateTimeKind.Utc).AddTicks(9909));

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "AppUserId", "IsActive", "IsMain", "ModifiedData", "PublicId", "Url" },
                values: new object[,]
                {
                    { 1, "d26d32da-d505-43b5-9e6d-51b667b27a0c", true, true, new DateOnly(1, 1, 1), "some-unique-public-id2", "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA" },
                    { 2, "160465c2-b0b2-4fb7-abe1-56ac9944b894", true, false, new DateOnly(1, 1, 1), "some-unique-public-id2", "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA" },
                    { 3, "2ff4a611-8f2c-4540-ace1-0da73cb212e0", true, false, new DateOnly(1, 1, 1), "some-unique-public-id3", "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 8, 21, 16, 23, 50, 685, DateTimeKind.Utc).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 8, 21, 16, 23, 50, 685, DateTimeKind.Utc).AddTicks(2128));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 8, 21, 16, 23, 50, 685, DateTimeKind.Utc).AddTicks(2130));
        }
    }
}
