using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Writing.Platform.Migrations
{
    /// <inheritdoc />
    public partial class updateLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogLikes_BlogPosts_BlogPostId",
                table: "blogLikes");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "blogLikes");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogPostId",
                table: "blogLikes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_blogLikes_BlogPosts_BlogPostId",
                table: "blogLikes",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogLikes_BlogPosts_BlogPostId",
                table: "blogLikes");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogPostId",
                table: "blogLikes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogId",
                table: "blogLikes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_blogLikes_BlogPosts_BlogPostId",
                table: "blogLikes",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }
    }
}
