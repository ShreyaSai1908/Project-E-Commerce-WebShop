using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class WebShop_Chg5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderHeaderOrderID",
                table: "GetProductList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GetProductList_OrderHeaderOrderID",
                table: "GetProductList",
                column: "OrderHeaderOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_GetProductList_OrderHeader_OrderHeaderOrderID",
                table: "GetProductList",
                column: "OrderHeaderOrderID",
                principalTable: "OrderHeader",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetProductList_OrderHeader_OrderHeaderOrderID",
                table: "GetProductList");

            migrationBuilder.DropIndex(
                name: "IX_GetProductList_OrderHeaderOrderID",
                table: "GetProductList");

            migrationBuilder.DropColumn(
                name: "OrderHeaderOrderID",
                table: "GetProductList");
        }
    }
}
