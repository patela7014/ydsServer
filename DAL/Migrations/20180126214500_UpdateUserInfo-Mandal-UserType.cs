using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class UpdateUserInfoMandalUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mandal",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mandal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AddedBy = table.Column<string>(nullable: true),
                    BirthDay = table.Column<string>(nullable: true),
                    BirthMonth = table.Column<string>(nullable: true),
                    BirthYear = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    Created = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    HomePhone = table.Column<string>(nullable: true),
                    MandalId = table.Column<string>(nullable: true),
                    MidName = table.Column<string>(nullable: true),
                    UserTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfo_User_AddedBy",
                        column: x => x.AddedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfo_Mandal_MandalId",
                        column: x => x.MandalId,
                        principalTable: "Mandal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_AddedBy",
                table: "UserInfo",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_MandalId",
                table: "UserInfo",
                column: "MandalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "Mandal");
        }
    }
}
