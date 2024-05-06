using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DocumentCatRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Documents_DocumentId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DocumentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentId",
                table: "Documents",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Categories_DocumentId",
                table: "Documents",
                column: "DocumentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Categories_DocumentId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_DocumentId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DocumentId",
                table: "Categories",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Documents_DocumentId",
                table: "Categories",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }
    }
}
