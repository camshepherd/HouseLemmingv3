using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseLemmingv3.Migrations
{
    public partial class Harold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvertId",
                table: "Requests",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
