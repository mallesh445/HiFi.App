using Microsoft.EntityFrameworkCore.Migrations;

namespace HiFi.Data.Migrations
{
    public partial class spcheckuserexistornot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var spUserExistsOrNot = @"CREATE PROCEDURE [dbo].[sp_CheckUserExistsOrNot]
                    @UserName varchar(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT TOP 1 * FROM AspNetUsers where UserName = @UserName
                END";
            migrationBuilder.Sql(spUserExistsOrNot);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[sp_CheckUserExistsOrNot]");
        }
    }
}
