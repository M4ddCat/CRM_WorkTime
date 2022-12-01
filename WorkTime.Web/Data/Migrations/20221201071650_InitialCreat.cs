using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Web.Data.Migrations
{
    public partial class InitialCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70cb9643-3ff0-49a2-9b0c-7896477bdb8f", null, "Bookkeeper", "BOOKKEEPER" },
                    { "90b3bef6-4316-4915-a65a-df20caffc0e4", null, "Administrator", "ADMINISTRATOR" },
                    { "b0b155ea-acc4-454b-8d3f-692d931f2104", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Сформирован" },
                    { 2, "Оплачен" },
                    { 3, "Подтверждён" }
                });

            migrationBuilder.InsertData(
                table: "TypeOfEmployment",
                columns: new[] { "Id", "Name", "Tax" },
                values: new object[,]
                {
                    { "1", "Устроен по ТК", 13 },
                    { "2", "Самозанятый", 13 }
                });

            migrationBuilder.InsertData(
                table: "WorkTaskStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Сформирована" },
                    { 2, "Выполняется" },
                    { 3, "Ожидает проверки" },
                    { 4, "Завершена" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70cb9643-3ff0-49a2-9b0c-7896477bdb8f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90b3bef6-4316-4915-a65a-df20caffc0e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0b155ea-acc4-454b-8d3f-692d931f2104");

            migrationBuilder.DeleteData(
                table: "PaymentStates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentStates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentStates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypeOfEmployment",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "TypeOfEmployment",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "WorkTaskStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkTaskStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkTaskStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkTaskStatuses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
