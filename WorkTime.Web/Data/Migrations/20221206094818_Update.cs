using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d9127e5-a033-4678-ba0b-1a56a2ab8040");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6859dd41-f209-42d6-b981-98c053216394");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4b6fc8f-e4ef-4e83-870f-b09d1e4a6913");

            migrationBuilder.CreateTable(
                name: "InvoiceFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    InvoiceId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceFiles_Invoices",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3220b5e0-6ffb-4cfe-9dea-2a8bd3817dc5", null, "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "32bca443-4c2f-4399-a2bd-2160097baa0c", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35ea7fe4-6f34-4541-81ae-939635a30a11", null, "Bookkeeper", "BOOKKEEPER" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiles_InvoiceId",
                table: "InvoiceFiles",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceFiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3220b5e0-6ffb-4cfe-9dea-2a8bd3817dc5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32bca443-4c2f-4399-a2bd-2160097baa0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35ea7fe4-6f34-4541-81ae-939635a30a11");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4d9127e5-a033-4678-ba0b-1a56a2ab8040", null, "Bookkeeper", "BOOKKEEPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6859dd41-f209-42d6-b981-98c053216394", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a4b6fc8f-e4ef-4e83-870f-b09d1e4a6913", null, "Manager", "MANAGER" });
        }
    }
}
