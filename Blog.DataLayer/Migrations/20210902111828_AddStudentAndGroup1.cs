using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class AddStudentAndGroup1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
