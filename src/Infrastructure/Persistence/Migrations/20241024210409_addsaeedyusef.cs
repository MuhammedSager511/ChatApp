using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addsaeedyusef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 10, 24, 21, 4, 7, 360, DateTimeKind.Utc).AddTicks(5261));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 10, 24, 21, 4, 7, 360, DateTimeKind.Utc).AddTicks(5279));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 10, 24, 21, 4, 7, 360, DateTimeKind.Utc).AddTicks(5281));

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppUserId", "PublicId", "Url" },
                values: new object[] { "da9b70b8-5425-4f8f-82c4-144706bb4a93", "some-unique-public-id55", "https://xsgames.co/randomusers/assets/avatars/male/31.jpg" });

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AppUserId", "IsMain", "PublicId", "Url" },
                values: new object[] { "da9b70b8-5425-4f8f-82c4-144706bb4a93", true, "some-unique-public-id5533", "https://xsgames.co/randomusers/assets/avatars/male/41.jpg" });

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AppUserId", "IsMain", "PublicId", "Url" },
                values: new object[] { "da9b70b8-5425-4f8f-82c4-144706bb4a93", true, "some-unique-public-id553", "https://xsgames.co/randomusers/assets/avatars/male/21.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppUserId", "PublicId", "Url" },
                values: new object[] { "d26d32da-d505-43b5-9e6d-51b667b27a0c", "some-unique-public-id2", "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA" });

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AppUserId", "IsMain", "PublicId", "Url" },
                values: new object[] { "160465c2-b0b2-4fb7-abe1-56ac9944b894", false, "some-unique-public-id2", "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA" });

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AppUserId", "IsMain", "PublicId", "Url" },
                values: new object[] { "2ff4a611-8f2c-4540-ace1-0da73cb212e0", false, "some-unique-public-id3", "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA" });
        }
    }
}
