using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCBlogApp.Migrations
{
    /// <inheritdoc />
    public partial class ImageModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostImages",
                newName: "FileSize");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "PostImages",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "PostImages",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "PostImages",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "PostImages");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "PostImages");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "PostImages");

            migrationBuilder.RenameColumn(
                name: "FileSize",
                table: "PostImages",
                newName: "PostId");
        }
    }
}
