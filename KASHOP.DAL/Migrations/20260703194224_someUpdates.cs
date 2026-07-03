using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KASHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class someUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_CatergoryTranslations_CategoryId",
                table: "CatergoryTranslations",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatergoryTranslations_Categories_CategoryId",
                table: "CatergoryTranslations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatergoryTranslations_Categories_CategoryId",
                table: "CatergoryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CatergoryTranslations_CategoryId",
                table: "CatergoryTranslations");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
