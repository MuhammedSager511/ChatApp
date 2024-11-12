using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Like : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLikes",
                columns: table => new
                {
                    SourceUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => new { x.SourceUserId, x.LikedUserId });
                    table.ForeignKey(
                        name: "FK_UserLikes_AspNetUsers_LikedUserId",
                        column: x => x.LikedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserLikes_AspNetUsers_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_LikedUserId",
                table: "UserLikes",
                column: "LikedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikes");

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
    }
}
