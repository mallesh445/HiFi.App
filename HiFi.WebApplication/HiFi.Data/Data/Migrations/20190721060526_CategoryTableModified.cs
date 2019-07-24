using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.WebApplication.Data.Migrations
{
    public partial class CategoryTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_Id",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Id",
                table: "Category",
                newName: "IX_Category_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_CreatedByUserId",
                table: "Category",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_CreatedByUserId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Category",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CreatedByUserId",
                table: "Category",
                newName: "IX_Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_Id",
                table: "Category",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
