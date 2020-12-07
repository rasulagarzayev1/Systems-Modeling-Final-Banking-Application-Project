using Microsoft.EntityFrameworkCore.Migrations;

namespace SysModelBank.Data.Migrations
{
    public partial class AddCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Symbol = table.Column<string>(nullable: false),
                    RateFromEur = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Name", "RateFromEur", "Symbol" },
                values: new object[] { 1, "EUR", 1m, "€" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Name", "RateFromEur", "Symbol" },
                values: new object[] { 2, "USD", 1.21307m, "$" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Name", "RateFromEur", "Symbol" },
                values: new object[] { 3, "NGN", 461.643m, "₦" });

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Name",
                table: "Currency",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
