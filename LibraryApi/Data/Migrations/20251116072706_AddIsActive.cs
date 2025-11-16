using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "user_images",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "genres",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "favorite_books",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "collection_books",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "collection",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "books",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "book_images",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "authors",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "author_images",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "user_images");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "favorite_books");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "collection_books");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "collection");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "books");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "book_images");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "author_images");
        }
    }
}
