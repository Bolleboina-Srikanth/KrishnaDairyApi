using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1New.Migrations
{
    public partial class addedNewColumnsInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ProductTable",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "ProductTable",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ProductTable");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductTable");
        }
    }
}
