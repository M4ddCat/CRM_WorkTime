using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class UserInfoPassUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "INN",
                table: "AspNetUserInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportGived",
                table: "AspNetUserInformation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "INN",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "PassportGived",
                table: "AspNetUserInformation");
        }
    }
}
