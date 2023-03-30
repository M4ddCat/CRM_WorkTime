using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class TemplateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateFile",
                table: "ContractTemplates");

            migrationBuilder.AddColumn<string>(
                name: "EmpTypeId",
                table: "ContractTemplates",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "ContractTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTemplates_EmpTypeId",
                table: "ContractTemplates",
                column: "EmpTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTemplates_EmpTypes",
                table: "ContractTemplates",
                column: "EmpTypeId",
                principalTable: "TypeOfEmployment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTemplates_EmpTypes",
                table: "ContractTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ContractTemplates_EmpTypeId",
                table: "ContractTemplates");

            migrationBuilder.DropColumn(
                name: "EmpTypeId",
                table: "ContractTemplates");

            migrationBuilder.DropColumn(
                name: "Template",
                table: "ContractTemplates");

            migrationBuilder.AddColumn<byte[]>(
                name: "TemplateFile",
                table: "ContractTemplates",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
