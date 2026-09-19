using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KASHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class auditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "createdById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updatedById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_createdById",
                table: "Categories",
                column: "createdById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_updatedById",
                table: "Categories",
                column: "updatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_createdById",
                table: "Categories",
                column: "createdById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_updatedById",
                table: "Categories",
                column: "updatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_createdById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_updatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_createdById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_updatedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "createdById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "updatedById",
                table: "Categories");
        }
    }
}
