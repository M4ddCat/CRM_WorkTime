using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class AddedContractTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerPersonPostInCompany",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TemplateFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTemplates_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTemplates_ProjectId",
                table: "ContractTemplates",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTemplates");

            migrationBuilder.DropColumn(
                name: "CustomerPersonPostInCompany",
                table: "Projects");
        }
    }
}
