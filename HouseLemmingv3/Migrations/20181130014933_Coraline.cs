using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseLemmingv3.Migrations
{
    public partial class Coraline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Adverts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_ApplicationUserId",
                table: "Adverts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_AspNetUsers_ApplicationUserId",
                table: "Adverts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_AspNetUsers_ApplicationUserId",
                table: "Adverts");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_ApplicationUserId",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Adverts");
        }
    }
}
