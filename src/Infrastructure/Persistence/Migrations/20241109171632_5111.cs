using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _5111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 16, 32, 183, DateTimeKind.Utc).AddTicks(1693));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 16, 32, 183, DateTimeKind.Utc).AddTicks(1709));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 16, 32, 183, DateTimeKind.Utc).AddTicks(1711));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 14, 25, 850, DateTimeKind.Utc).AddTicks(9626));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 14, 25, 850, DateTimeKind.Utc).AddTicks(9638));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 14, 25, 850, DateTimeKind.Utc).AddTicks(9640));
        }
    }
}
