using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductCategoryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryCategoryID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryCategoryID",
                table: "Products",
                column: "ProductCategoryCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryCategoryID",
                table: "Products",
                column: "ProductCategoryCategoryID",
                principalTable: "ProductCategories",
                principalColumn: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryCategoryID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryCategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryCategoryID",
                table: "Products");
        }
    }
}
