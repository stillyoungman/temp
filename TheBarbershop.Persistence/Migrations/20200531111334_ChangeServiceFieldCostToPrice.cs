using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBarbershop.Persistence.Migrations
{
    public partial class ChangeServiceFieldCostToPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Service");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Service",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Service");

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Service",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
