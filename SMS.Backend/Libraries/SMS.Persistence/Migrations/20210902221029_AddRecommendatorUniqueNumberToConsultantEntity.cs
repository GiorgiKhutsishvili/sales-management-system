using Microsoft.EntityFrameworkCore.Migrations;

namespace SMS.Persistence.Migrations
{
    public partial class AddRecommendatorUniqueNumberToConsultantEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecomendatorId",
                table: "Consultants",
                newName: "RecommendatorId");

            migrationBuilder.AddColumn<string>(
                name: "RecommendatorUniqueNumber",
                table: "Consultants",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendatorUniqueNumber",
                table: "Consultants");

            migrationBuilder.RenameColumn(
                name: "RecommendatorId",
                table: "Consultants",
                newName: "RecomendatorId");
        }
    }
}
