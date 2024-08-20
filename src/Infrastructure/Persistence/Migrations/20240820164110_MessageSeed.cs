using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MessageSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "DateRead", "IsActive", "MessageSend", "ModifiedData", "RecipientDeleted", "RecipientId", "RecipientUserName", "SenderDeleted", "SenderId", "SenderUserName" },
                values: new object[,]
                {
                    { 1, "test-1", null, true, new DateTime(2024, 8, 20, 16, 41, 8, 878, DateTimeKind.Utc).AddTicks(3875), new DateOnly(1, 1, 1), false, 1, "Ali", false, 1, "Muhammed" },
                    { 2, "test-2", null, true, new DateTime(2024, 8, 20, 16, 41, 8, 878, DateTimeKind.Utc).AddTicks(3902), new DateOnly(1, 1, 1), false, 2, "Ali", false, 2, "Abdo" },
                    { 3, "test-3", null, true, new DateTime(2024, 8, 20, 16, 41, 8, 878, DateTimeKind.Utc).AddTicks(3905), new DateOnly(1, 1, 1), false, 3, "Abdo", false, 3, "Muhammed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
