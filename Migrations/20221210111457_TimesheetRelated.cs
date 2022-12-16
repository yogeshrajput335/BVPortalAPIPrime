using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BVPortalApi.Migrations
{
    public partial class TimesheetRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Timesheet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Timesheet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Timesheet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Timesheet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Timesheet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientContact",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientMail",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Candidates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rate",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Technology",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorContact",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorMail",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Visa",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Timesheet");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Timesheet");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Timesheet");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Timesheet");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Timesheet");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "ClientContact",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "ClientMail",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Technology",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "VendorContact",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "VendorMail",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Visa",
                table: "Candidates");
        }
    }
}
