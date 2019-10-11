using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.Data.Migrations
{
    public partial class datatypeChangeOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CreatedByUserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "OrderDetails",
                newName: "Quantity");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CreatedByUserId",
                table: "Orders",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CreatedByUserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderDetails",
                newName: "Count");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CreatedByUserId",
                table: "Orders",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
