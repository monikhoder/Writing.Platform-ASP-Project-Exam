using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Writing.Platform.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostGenre_Genres_GernesId",
                table: "BlogPostGenre");

            migrationBuilder.RenameColumn(
                name: "GernesId",
                table: "BlogPostGenre",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostGenre_GernesId",
                table: "BlogPostGenre",
                newName: "IX_BlogPostGenre_GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostGenre_Genres_GenresId",
                table: "BlogPostGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostGenre_Genres_GenresId",
                table: "BlogPostGenre");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "BlogPostGenre",
                newName: "GernesId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostGenre_GenresId",
                table: "BlogPostGenre",
                newName: "IX_BlogPostGenre_GernesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostGenre_Genres_GernesId",
                table: "BlogPostGenre",
                column: "GernesId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
