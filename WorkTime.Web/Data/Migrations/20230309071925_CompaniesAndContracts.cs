using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class CompaniesAndContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankInfoId",
                table: "AspNetUserInformation",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "AspNetUserInformation",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "AspNetUserInformation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "AspNetUserInformation",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNum",
                table: "AspNetUserInformation",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalAddress",
                table: "AspNetUserInformation",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialNetworkContact",
                table: "AspNetUserInformation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "AspNetUserInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankAccount = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankLocation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CorInv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BIK = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankInfoId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DirectorFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_BankInformation",
                        column: x => x.BankInfoId,
                        principalTable: "BankInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PerformerPersonId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PerformerCompanyId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CustomerPersonId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CustomerCompanyId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UserProjectId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ContractDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers",
                        column: x => x.PerformerPersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers1",
                        column: x => x.CustomerPersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_Companies",
                        column: x => x.PerformerCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_Companies1",
                        column: x => x.CustomerCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_UserProjects",
                        column: x => x.UserProjectId,
                        principalTable: "UserProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserInformation_BankInfoId",
                table: "AspNetUserInformation",
                column: "BankInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserInformation_CompanyId",
                table: "AspNetUserInformation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserInformation_UserTypeId",
                table: "AspNetUserInformation",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_BankInfoId",
                table: "Company",
                column: "BankInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerCompanyId",
                table: "Contract",
                column: "CustomerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerPersonId",
                table: "Contract",
                column: "CustomerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_PerformerCompanyId",
                table: "Contract",
                column: "PerformerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_PerformerPersonId",
                table: "Contract",
                column: "PerformerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_UserProjectId",
                table: "Contract",
                column: "UserProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserInformation_BankInformation",
                table: "AspNetUserInformation",
                column: "BankInfoId",
                principalTable: "BankInformation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserInformation_Companies",
                table: "AspNetUserInformation",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserInformation_UserType",
                table: "AspNetUserInformation",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id");

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Type", "PersonType" },
                values: new object[] { 1, "Исполнитель", "Физическое лицо" });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Type", "PersonType" },
                values: new object[] { 2, "Исполнитель", "Юридическое лицо" });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Type", "PersonType" },
                values: new object[] { 3, "Заказчик", "Физическое лицо" });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Type", "PersonType" },
                values: new object[] { 4, "Заказчик", "Юридическое лицо" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserInformation_BankInformation",
                table: "AspNetUserInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserInformation_Companies",
                table: "AspNetUserInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserInformation_UserType",
                table: "AspNetUserInformation");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "BankInformation");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserInformation_BankInfoId",
                table: "AspNetUserInformation");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserInformation_CompanyId",
                table: "AspNetUserInformation");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserInformation_UserTypeId",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "BankInfoId",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "PassportNum",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "PersonalAddress",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "SocialNetworkContact",
                table: "AspNetUserInformation");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "AspNetUserInformation");
        }
    }
}
