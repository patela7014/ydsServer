using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class AddSabhaUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserType",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "SabhaType",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Sabha",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Mandal",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Event",
                newName: "IsActive");

            migrationBuilder.CreateTable(
                name: "SabhaUsers",
                columns: table => new
                {
                    SabhaId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SabhaUsers", x => new { x.SabhaId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SabhaUsers_Sabha_SabhaId",
                        column: x => x.SabhaId,
                        principalTable: "Sabha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SabhaUsers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SabhaUsers_UserId",
                table: "SabhaUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SabhaUsers");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "UserType",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "SabhaType",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Sabha",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Mandal",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Event",
                newName: "Status");
        }
    }
}
