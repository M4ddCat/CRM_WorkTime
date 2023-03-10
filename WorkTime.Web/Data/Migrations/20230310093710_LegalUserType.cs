using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class LegalUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Contract",
            //    table: "Contract");

            // migrationBuilder.DropPrimaryKey(
            //    name: "PK_Company",
            //    table: "Company");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "UserType");

            //migrationBuilder.RenameTable(
            //    name: "Contract",
            //    newName: "Contracts");

            //migrationBuilder.RenameTable(
            //    name: "Company",
            //    newName: "Companies");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Contract_UserProjectId",
            //    table: "Contracts",
            //    newName: "IX_Contracts_UserProjectId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Contract_PerformerPersonId",
            //    table: "Contracts",
            //    newName: "IX_Contracts_PerformerPersonId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Contract_PerformerCompanyId",
            //    table: "Contracts",
            //    newName: "IX_Contracts_PerformerCompanyId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Contract_CustomerPersonId",
            //    table: "Contracts",
            //    newName: "IX_Contracts_CustomerPersonId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Contract_CustomerCompanyId",
            //    table: "Contracts",
            //    newName: "IX_Contracts_CustomerCompanyId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Company_BankInfoId",
            //    table: "Companies",
            //    newName: "IX_Companies_BankInfoId");

            migrationBuilder.AddColumn<int>(
                name: "LegalUserTypeId",
                table: "AspNetUserInformation",
                type: "int",
                nullable: true);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Contracts",
            //    table: "Contracts",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Companies",
            //    table: "Companies",
            //    column: "Id");

            migrationBuilder.CreateTable(
                name: "LegalUserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LegalType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalUserType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserInformation_LegalUserTypeId",
                table: "AspNetUserInformation",
                column: "LegalUserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserInformation_LegalUserType",
                table: "AspNetUserInformation",
                column: "LegalUserTypeId",
                principalTable: "LegalUserType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserInformation_LegalUserType",
                table: "AspNetUserInformation");

            migrationBuilder.DropTable(
                name: "LegalUserType");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserInformation_LegalUserTypeId",
                table: "AspNetUserInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LegalUserTypeId",
                table: "AspNetUserInformation");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_UserProjectId",
                table: "Contract",
                newName: "IX_Contract_UserProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_PerformerPersonId",
                table: "Contract",
                newName: "IX_Contract_PerformerPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_PerformerCompanyId",
                table: "Contract",
                newName: "IX_Contract_PerformerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerPersonId",
                table: "Contract",
                newName: "IX_Contract_CustomerPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerCompanyId",
                table: "Contract",
                newName: "IX_Contract_CustomerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_BankInfoId",
                table: "Company",
                newName: "IX_Company_BankInfoId");

            migrationBuilder.AddColumn<string>(
                name: "PersonType",
                table: "UserType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");
        }
    }
}
