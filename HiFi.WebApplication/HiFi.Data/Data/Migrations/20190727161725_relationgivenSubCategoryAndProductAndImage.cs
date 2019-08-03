using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.WebApplication.Data.Migrations
{
    public partial class relationgivenSubCategoryAndProductAndImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "SubCategoryOne",
                nullable: true);

            //by mallesh unneccessarly generated this property
            //migrationBuilder.AddColumn<int>(
            //    name: "EId",
            //    table: "SubCategoryOne",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SC_ImageName",
                table: "SubCategoryOne",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_ImagePath",
                table: "SubCategoryOne",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "SubCategoryOne",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "ProductImage",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductImage",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //by mallesh unneccessarly generated this property
            //migrationBuilder.AddColumn<int>(
            //    name: "EId",
            //    table: "ProductImage",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKProductId",
                table: "ProductImage",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductImage",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryOneId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EId",
                table: "PictureBinary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryOne_CreatedByUserId",
                table: "SubCategoryOne",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryOne_UpdatedByUserId",
                table: "SubCategoryOne",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_FKProductId",
                table: "ProductImage",
                column: "FKProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubCategoryOneId",
                table: "Product",
                column: "SubCategoryOneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SubCategoryOne_SubCategoryOneId",
                table: "Product",
                column: "SubCategoryOneId",
                principalTable: "SubCategoryOne",
                principalColumn: "SubCategoryOneId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_FKProductId",
                table: "ProductImage",
                column: "FKProductId",
                principalTable: "Product",
                principalColumn: "PKProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryOne_AspNetUsers_CreatedByUserId",
                table: "SubCategoryOne",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryOne_AspNetUsers_UpdatedByUserId",
                table: "SubCategoryOne",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_SubCategoryOne_SubCategoryOneId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_FKProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryOne_AspNetUsers_CreatedByUserId",
                table: "SubCategoryOne");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryOne_AspNetUsers_UpdatedByUserId",
                table: "SubCategoryOne");

            migrationBuilder.DropIndex(
                name: "IX_SubCategoryOne_CreatedByUserId",
                table: "SubCategoryOne");

            migrationBuilder.DropIndex(
                name: "IX_SubCategoryOne_UpdatedByUserId",
                table: "SubCategoryOne");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_FKProductId",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_Product_SubCategoryOneId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "SubCategoryOne");

            //migrationBuilder.DropColumn(
            //    name: "EId",
            //    table: "SubCategoryOne");

            migrationBuilder.DropColumn(
                name: "SC_ImageName",
                table: "SubCategoryOne");

            migrationBuilder.DropColumn(
                name: "SC_ImagePath",
                table: "SubCategoryOne");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "SubCategoryOne");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductImage");

            //migrationBuilder.DropColumn(
            //    name: "EId",
            //    table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "FKProductId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "EId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SubCategoryOneId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "EId",
                table: "PictureBinary");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "ProductImage",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
