using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBarbershop.Persistence.Migrations
{
    public partial class ExtendUserModelAndMaterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Masters");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Masters",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Masters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Masters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Masters",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Administrators",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Administrators");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Masters",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
