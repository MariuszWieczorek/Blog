using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class AddLongDescriptionInPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(@"UPDATE POSTS SET LongDescription = Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Posts");
        }
    }
}
