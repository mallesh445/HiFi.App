using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.Data.Migrations
{
    public partial class ModelNumberSerailNum_addedToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModelNumber",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelNumber",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Product");
        }
    }
}
