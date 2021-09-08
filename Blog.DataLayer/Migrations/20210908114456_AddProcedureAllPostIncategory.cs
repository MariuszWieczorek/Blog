using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class AddProcedureAllPostIncategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR ALTER PROCEDURE dbo.AllPostInCategory
                @id int
            AS

            SELECT Id, Title2, Description, Url, Published, PostedOn, Modified, Type, ShortDescription, ImageUrl, CategoryId, UserId, ApprovedByUserId
            FROM dbo.Posts2
            WHERE CategoryId=@id ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.AllPostInCategory");
        }
    }
}
