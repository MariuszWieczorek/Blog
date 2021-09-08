using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class AddProcedureDeleteArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

            CREATE OR ALTER PROCEDURE dbo.DeleteArticle
                @id int
            AS

            DELETE FROM Posts2
            WHERE Id=@id

            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.DeleteArticle");
        }
    }
}
