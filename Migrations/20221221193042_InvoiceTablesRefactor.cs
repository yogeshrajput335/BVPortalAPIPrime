using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BVPortalApi.Migrations
{
    public partial class InvoiceTablesRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Employee_EmployeeId",
                table: "InvoiceProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Invoice_InvoiceId",
                table: "InvoiceProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Project_ProjectId",
                table: "InvoiceProduct");

            migrationBuilder.RenameColumn(
                name: "TotalHours",
                table: "InvoiceProduct",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "InvoiceProduct",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "PerHourCost",
                table: "InvoiceProduct",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "InvoiceNo",
                table: "Invoice",
                newName: "InvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "FromLine3",
                table: "Invoice",
                newName: "NoteToCustomer");

            migrationBuilder.RenameColumn(
                name: "FromLine2",
                table: "Invoice",
                newName: "GetPaidNotes");

            migrationBuilder.RenameColumn(
                name: "FromLine1",
                table: "Invoice",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Invoice",
                newName: "InvoiceDate");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "InvoiceProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InvoiceProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "InvoiceProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsProduct",
                table: "InvoiceProduct",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ItemTypeId",
                table: "InvoiceProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "InvoiceProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "InvoiceProduct",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "InvoiceProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Term",
                table: "Invoice",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Invoice",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Invoice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddressLine1",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddressLine2",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddressLine3",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyEmailAddress",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyPhoneNumber",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddressLine1",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddressLine2",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddressLine3",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ProductId",
                table: "InvoiceProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ServiceId",
                table: "InvoiceProduct",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CompanyId",
                table: "Invoice",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Company_CompanyId",
                table: "Invoice",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Customer_CustomerId",
                table: "Invoice",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Employee_EmployeeId",
                table: "InvoiceProduct",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Invoice_InvoiceId",
                table: "InvoiceProduct",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Product_ProductId",
                table: "InvoiceProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Project_ProjectId",
                table: "InvoiceProduct",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Service_ServiceId",
                table: "InvoiceProduct",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Company_CompanyId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Customer_CustomerId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Employee_EmployeeId",
                table: "InvoiceProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Invoice_InvoiceId",
                table: "InvoiceProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Product_ProductId",
                table: "InvoiceProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceProduct_Project_ProjectId",
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

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CompanyId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IsProduct",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "InvoiceProduct");

            migrationBuilder.DropColumn(
                name: "CompanyAddressLine1",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CompanyAddressLine2",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CompanyAddressLine3",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CompanyEmailAddress",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CompanyPhoneNumber",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerAddressLine1",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerAddressLine2",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerAddressLine3",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "InvoiceProduct",
                newName: "TotalCost");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "InvoiceProduct",
                newName: "TotalHours");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "InvoiceProduct",
                newName: "PerHourCost");

            migrationBuilder.RenameColumn(
                name: "NoteToCustomer",
                table: "Invoice",
                newName: "FromLine3");

            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "Invoice",
                newName: "InvoiceNo");

            migrationBuilder.RenameColumn(
                name: "InvoiceDate",
                table: "Invoice",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "GetPaidNotes",
                table: "Invoice",
                newName: "FromLine2");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Invoice",
                newName: "FromLine1");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "InvoiceProduct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InvoiceProduct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "InvoiceProduct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Term",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Invoice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Employee_EmployeeId",
                table: "InvoiceProduct",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Invoice_InvoiceId",
                table: "InvoiceProduct",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceProduct_Project_ProjectId",
                table: "InvoiceProduct",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
