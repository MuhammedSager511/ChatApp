using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class nullbel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "MessageSend",
                value: new DateTime(2024, 10, 28, 17, 19, 28, 900, DateTimeKind.Utc).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "MessageSend",
                value: new DateTime(2024, 10, 28, 17, 19, 28, 900, DateTimeKind.Utc).AddTicks(8007));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "MessageSend",
                value: new DateTime(2024, 10, 28, 17, 19, 28, 900, DateTimeKind.Utc).AddTicks(8009));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
