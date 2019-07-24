using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.WebApplication.Data.Migrations
{
    public partial class addingAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    ZipPostalCode = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    FaxNumber = table.Column<string>(nullable: true),
                    CustomAttributes = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    StateOrProvince = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
