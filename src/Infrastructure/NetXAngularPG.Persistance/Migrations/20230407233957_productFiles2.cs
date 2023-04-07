using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetXAngularPG.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class productFiles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProductProductImageFile",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductImageFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductImageFile", x => new { x.ProductId, x.ProductImageFilesId });
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_Files_ProductImageFilesId",
                        column: x => x.ProductImageFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductImageFile_ProductImageFilesId",
                table: "ProductProductImageFile",
                column: "ProductImageFilesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductImageFile");

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
    }
}
