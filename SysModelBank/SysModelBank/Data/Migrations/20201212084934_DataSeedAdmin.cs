using Microsoft.EntityFrameworkCore.Migrations;

namespace SysModelBank.Data.Migrations
{
    public partial class DataSeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_UserId",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CurrencyId", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { 999, 0, "Tartu", "da7a4f42-ff3c-42a8-935a-62af68f978b0", 1, "admin@admin.ee", false, "admin", "admin", false, null, "ADMIN@ADMIN.EE", "ADMIN", "AQAAAAEAACcQAAAAEHsMGzjMkGokHqRjfxWTubUykxdrCkvbl0dt148ZjbUpQjyMiCseYeXZNPoDHj5SEw==", null, false, "ce907fd5-ccb4-4e96-a7ea-45712a14f5ef", 1, false, "ADMIN" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { 999, 0m, 999 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 999, 2 });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_UserId",
                table: "Accounts");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 999, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
