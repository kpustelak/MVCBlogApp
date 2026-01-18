using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCBlogApp.Migrations
{
    /// <inheritdoc />
    public partial class FavoritePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryId",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Posts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryId",
                table: "Posts",
                column: "PostCategoryId",
                principalTable: "PostCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryId",
                table: "Posts",
                column: "PostCategoryId",
                principalTable: "PostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
