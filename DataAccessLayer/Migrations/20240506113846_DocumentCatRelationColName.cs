using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DocumentCatRelationColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Categories_DocumentId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Documents",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_DocumentId",
                table: "Documents",
                newName: "IX_Documents_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Categories_CategoryId",
                table: "Documents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Categories_CategoryId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Documents",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_CategoryId",
                table: "Documents",
                newName: "IX_Documents_DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Categories_DocumentId",
                table: "Documents",
                column: "DocumentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
