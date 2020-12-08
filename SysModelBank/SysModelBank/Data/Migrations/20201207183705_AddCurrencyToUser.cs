using Microsoft.EntityFrameworkCore.Migrations;

namespace SysModelBank.Data.Migrations
{
    public partial class AddCurrencyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // All existing data have deafult value of 1
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrencyId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrencyId",
                value: 1);

            // Reomve default value
            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "AspNetUsers",
                defaultValue: null,
                oldDefaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrencyId",
                table: "AspNetUsers",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Currency_CurrencyId",
                table: "AspNetUsers",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Currency_CurrencyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrencyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "AspNetUsers");
        }
    }
}
