using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCBlogApp.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedPostImageById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedImageUrl",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "PostCategoryId",
                table: "Posts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "FeaturedImageId",
                table: "Posts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_FeaturedImageId",
                table: "Posts",
                column: "FeaturedImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostImages_FeaturedImageId",
                table: "Posts",
                column: "FeaturedImageId",
                principalTable: "PostImages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostImages_FeaturedImageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_FeaturedImageId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FeaturedImageId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "PostCategoryId",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeaturedImageUrl",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
