using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorePractice.Migrations
{
    public partial class Initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Categories");
        }
    }
}
