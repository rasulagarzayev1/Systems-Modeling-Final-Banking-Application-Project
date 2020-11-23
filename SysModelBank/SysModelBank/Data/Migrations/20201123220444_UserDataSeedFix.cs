using Microsoft.EntityFrameworkCore.Migrations;

namespace SysModelBank.Data.Migrations
{
    public partial class UserDataSeedFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c852cf43-ad51-4ad8-91ff-be151f3d54e0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4b60cd42-f9c6-40e7-818f-b822551ba187");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0e6db1c1-7b87-4037-a5ef-c8fded4cfa80", "b3af7d63-7742-4060-85ba-dd41e57f6265" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "03860d84-55ed-4902-8ef2-27d45911964b", "3cbf6e9f-9c59-4674-90e7-ec85ce898399" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b3e9376a-4c12-4daa-8cb8-16f00a89d113");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8c89d318-c06a-4743-bfdd-29a358f62f50");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1c8a48d9-8047-4d1c-98b6-7c1bb12191f2", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "41b30ecf-37a9-429d-b46f-8c7ec7c31f0b", null });
        }
    }
}
