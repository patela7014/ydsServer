using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class AddSabha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SabhaType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SabhaType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sabha",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    SabhaTypeId = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sabha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sabha_SabhaType_SabhaTypeId",
                        column: x => x.SabhaTypeId,
                        principalTable: "SabhaType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sabha_SabhaTypeId",
                table: "Sabha",
                column: "SabhaTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sabha");

            migrationBuilder.DropTable(
                name: "SabhaType");
        }
    }
}
