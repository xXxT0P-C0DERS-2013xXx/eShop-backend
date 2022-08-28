using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistence.Migrations
{
    public partial class AddIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Item_Id",
                table: "Item",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Title",
                table: "Item",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Id",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Title",
                table: "Category",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Item_Id",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_Title",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Category_Id",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_Title",
                table: "Category");
        }
    }
}
