using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarEnthusiast.Migrations
{
    /// <inheritdoc />
    public partial class GroupDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "GroupImage",
                table: "Groups",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupImage",
                table: "Groups");
        }
    }
}
