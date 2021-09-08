using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class CreateUserFullInfoView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var userFullInfoView = @"
                CREATE OR ALTER VIEW [dbo].[UserFullInfo]
                AS
                SELECT u.Id, u.Login, ci.Email
                FROM dbo.Users as u
                LEFT JOIN dbo.ContactInfo as ci
                ON ci.UserId = u.Id";

            migrationBuilder.Sql(userFullInfoView);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW [dbo].[UserFullInfo]");
        }
    }
}
