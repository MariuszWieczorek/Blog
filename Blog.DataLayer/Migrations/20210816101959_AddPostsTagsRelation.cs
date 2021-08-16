using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataLayer.Migrations
{
    public partial class AddPostsTagsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts2_PostsId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagsId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostsTagMaps");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagsId",
                table: "PostsTagMaps",
                newName: "IX_PostsTagMaps_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostsTagMaps",
                table: "PostsTagMaps",
                columns: new[] { "PostsId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTagMaps_Posts2_PostsId",
                table: "PostsTagMaps",
                column: "PostsId",
                principalTable: "Posts2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTagMaps_Tags_TagsId",
                table: "PostsTagMaps",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsTagMaps_Posts2_PostsId",
                table: "PostsTagMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsTagMaps_Tags_TagsId",
                table: "PostsTagMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostsTagMaps",
                table: "PostsTagMaps");

            migrationBuilder.RenameTable(
                name: "PostsTagMaps",
                newName: "PostTag");

            migrationBuilder.RenameIndex(
                name: "IX_PostsTagMaps_TagsId",
                table: "PostTag",
                newName: "IX_PostTag_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                columns: new[] { "PostsId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts2_PostsId",
                table: "PostTag",
                column: "PostsId",
                principalTable: "Posts2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagsId",
                table: "PostTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
