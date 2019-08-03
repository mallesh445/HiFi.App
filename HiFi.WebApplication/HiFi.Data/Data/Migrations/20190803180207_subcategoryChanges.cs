using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.WebApplication.Data.Migrations
{
    public partial class subcategoryChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_SubCategoryOne_SubCategoryOneId",
                table: "Product");

            //migrationBuilder.DropColumn(
            //    name: "EId",
            //    table: "SubCategoryOne");

            //migrationBuilder.DropColumn(
            //    name: "EId",
            //    table: "ProductImage");

            //migrationBuilder.DropColumn(
            //    name: "EId",
            //    table: "Product");

            //migrationBuilder.DropColumn(
            //    name: "EId",
            //    table: "PictureBinary");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryOneId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SubCategoryOne_SubCategoryOneId",
                table: "Product",
                column: "SubCategoryOneId",
                principalTable: "SubCategoryOne",
                principalColumn: "SubCategoryOneId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_SubCategoryOne_SubCategoryOneId",
                table: "Product");

            //migrationBuilder.AddColumn<int>(
            //    name: "EId",
            //    table: "SubCategoryOne",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "EId",
            //    table: "ProductImage",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AlterColumn<int>(
            //    name: "SubCategoryOneId",
            //    table: "Product",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AddColumn<int>(
            //    name: "EId",
            //    table: "Product",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "EId",
            //    table: "PictureBinary",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SubCategoryOne_SubCategoryOneId",
                table: "Product",
                column: "SubCategoryOneId",
                principalTable: "SubCategoryOne",
                principalColumn: "SubCategoryOneId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
