using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseLemmingv3.Migrations
{
    public partial class Barry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Adverts_AdvertId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_AdvertId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AdvertId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Adverts");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Adverts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "AdvertId",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Adverts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AdvertId",
                table: "Requests",
                column: "AdvertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Adverts_AdvertId",
                table: "Requests",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "AdvertId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
