using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class AddPostApprovedByUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedByUserId",
                table: "Posts2",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts2_ApprovedByUserId",
                table: "Posts2",
                column: "ApprovedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts2_Users_ApprovedByUserId",
                table: "Posts2",
                column: "ApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts2_Users_ApprovedByUserId",
                table: "Posts2");

            migrationBuilder.DropIndex(
                name: "IX_Posts2_ApprovedByUserId",
                table: "Posts2");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserId",
                table: "Posts2");
        }
    }
}
