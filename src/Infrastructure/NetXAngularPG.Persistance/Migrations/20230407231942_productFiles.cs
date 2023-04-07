using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetXAngularPG.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class productFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductImageFileId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductImageFileId",
                table: "Products",
                column: "ProductImageFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Files_ProductImageFileId",
                table: "Products",
                column: "ProductImageFileId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Files_ProductImageFileId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductImageFileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImageFileId",
                table: "Products");
        }
    }
}
