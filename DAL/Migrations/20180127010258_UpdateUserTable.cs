using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class UpdateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthDay",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthMonth",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthYear",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Created",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomePhone",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MandalId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MidName",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserTypeId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_AddedBy",
                table: "User",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_MandalId",
                table: "User",
                column: "MandalId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_AddedBy",
                table: "User",
                column: "AddedBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Mandal_MandalId",
                table: "User",
                column: "MandalId",
                principalTable: "Mandal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserType_UserTypeId",
                table: "User",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_AddedBy",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Mandal_MandalId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserType_UserTypeId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AddedBy",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MandalId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserTypeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BirthMonth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HomePhone",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MandalId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MidName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "User");

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
    }
}
