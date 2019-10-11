using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.Data.Migrations
{
    public partial class secondInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_FKProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryOne_Category_CategoryId",
                table: "SubCategoryOne");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "SubCategoryOne",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FKProductId",
                table: "ProductImage",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_FKProductId",
                table: "ProductImage",
                column: "FKProductId",
                principalTable: "Product",
                principalColumn: "PKProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryOne_Category_CategoryId",
                table: "SubCategoryOne",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_FKProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryOne_Category_CategoryId",
                table: "SubCategoryOne");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "SubCategoryOne",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FKProductId",
                table: "ProductImage",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_FKProductId",
                table: "ProductImage",
                column: "FKProductId",
                principalTable: "Product",
                principalColumn: "PKProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryOne_Category_CategoryId",
                table: "SubCategoryOne",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
