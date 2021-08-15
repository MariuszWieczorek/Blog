using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class RemoveLongDescriptionInPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
