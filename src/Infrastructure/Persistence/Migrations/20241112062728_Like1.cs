using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Like1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 11, 12, 6, 27, 27, 709, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 11, 12, 6, 27, 27, 709, DateTimeKind.Utc).AddTicks(5637));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 11, 12, 6, 27, 27, 709, DateTimeKind.Utc).AddTicks(5639));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 19, 8, 919, DateTimeKind.Utc).AddTicks(7137));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 19, 8, 919, DateTimeKind.Utc).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 11, 9, 17, 19, 8, 919, DateTimeKind.Utc).AddTicks(7156));
        }
    }
}
