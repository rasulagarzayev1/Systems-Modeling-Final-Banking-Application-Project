using Microsoft.EntityFrameworkCore.Migrations;

namespace SysModelBank.Data.Migrations
{
    public partial class FixedConcurrencyStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "df1ac39d-c136-4bb9-abd4-8f966b15a8c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9f2cef27-c862-427d-ae0e-19d40c718ed4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "aaa20ff6-6bf2-4f3c-897f-6bf35568bf3b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "393b36c9-83d5-4d5c-82e1-34a0d8551a68");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9f2cef27-c862-427d-ae0e-19d40c718ed4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "df1ac39d-c136-4bb9-abd4-8f966b15a8c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "393b36c9-83d5-4d5c-82e1-34a0d8551a68");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "aaa20ff6-6bf2-4f3c-897f-6bf35568bf3b");
        }
    }
}
