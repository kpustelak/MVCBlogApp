using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCBlogApp.Migrations
{
    /// <inheritdoc />
    public partial class ExcerptInPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Excerpt",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excerpt",
                table: "Posts");
        }
    }
}
