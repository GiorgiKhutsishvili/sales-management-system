using Microsoft.EntityFrameworkCore.Migrations;

namespace SMS.Persistence.Migrations
{
    public partial class fixtableserror : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalesProducts_ProductId",
                table: "SalesProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesProducts_Products_ProductId",
                table: "SalesProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesProducts_Products_ProductId",
                table: "SalesProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts");

            migrationBuilder.DropIndex(
                name: "IX_SalesProducts_ProductId",
                table: "SalesProducts");
        }
    }
}
