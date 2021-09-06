using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMS.Persistence.Migrations
{
    public partial class RemoveDateDeletedToSaleProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "SalesProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "SalesProducts",
                type: "datetime2",
                nullable: true);
        }
    }
}
