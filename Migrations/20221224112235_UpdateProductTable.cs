using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BVPortalApi.Migrations
{
    public partial class UpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Product_ProductId",
                table: "InvoiceProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Service_ServiceId",
                table: "InvoiceProduct");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceProduct_ProductId",
                table: "InvoiceProduct");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceProduct_ServiceId",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "InvoiceProduct");

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "InvoiceProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "InvoiceProduct",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "InvoiceProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "InvoiceProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "InvoiceProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ProductId",
                table: "InvoiceProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ServiceId",
                table: "InvoiceProduct",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Product_ProductId",
                table: "InvoiceProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Service_ServiceId",
                table: "InvoiceProduct",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
