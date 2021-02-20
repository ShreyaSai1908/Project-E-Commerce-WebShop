using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class WebShop_Chg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeader_orderHeaderOrderID",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "orderHeaderOrderID",
                table: "OrderDetails",
                newName: "OrderHeaderOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_orderHeaderOrderID",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderHeaderOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeader_OrderHeaderOrderID",
                table: "OrderDetails",
                column: "OrderHeaderOrderID",
                principalTable: "OrderHeader",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeader_OrderHeaderOrderID",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderHeaderOrderID",
                table: "OrderDetails",
                newName: "orderHeaderOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderHeaderOrderID",
                table: "OrderDetails",
                newName: "IX_OrderDetails_orderHeaderOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeader_orderHeaderOrderID",
                table: "OrderDetails",
                column: "orderHeaderOrderID",
                principalTable: "OrderHeader",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
