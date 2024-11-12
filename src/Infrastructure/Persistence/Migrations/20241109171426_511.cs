using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _511 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "DateRead", "IsActive", "MessageSend", "ModifiedData", "RecipientDeleted", "RecipientId", "RecipientUserName", "SenderDeleted", "SenderId", "SenderUserName" },
                values: new object[,]
                {
                    { 1, "test-1", null, true, new DateTime(2024, 11, 9, 17, 14, 25, 850, DateTimeKind.Utc).AddTicks(9626), new DateOnly(1, 1, 1), false, 11, "Ali", false, 11, "Muhammed" },
                    { 2, "test-2", null, true, new DateTime(2024, 11, 9, 17, 14, 25, 850, DateTimeKind.Utc).AddTicks(9638), new DateOnly(1, 1, 1), false, 12, "Ali", false, 12, "Abdo" },
                    { 3, "test-3", null, true, new DateTime(2024, 11, 9, 17, 14, 25, 850, DateTimeKind.Utc).AddTicks(9640), new DateOnly(1, 1, 1), false, 13, "Abdo", false, 13, "Muhammed" }
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
