using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class ProjectCustomerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCompanyId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPersonId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerCompanyId",
                table: "Projects",
                column: "CustomerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerPersonId",
                table: "Projects",
                column: "CustomerPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Companies",
                table: "Projects",
                column: "CustomerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Persons",
                table: "Projects",
                column: "CustomerPersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Companies",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Persons",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CustomerCompanyId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CustomerPersonId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CustomerCompanyId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CustomerPersonId",
                table: "Projects");
        }
    }
}
