using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1New.Migrations
{
    public partial class updatedusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customertable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Customertable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserPhoto",
                table: "Customertable",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customertable");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customertable");

            migrationBuilder.DropColumn(
                name: "UserPhoto",
                table: "Customertable");
        }
    }
}
