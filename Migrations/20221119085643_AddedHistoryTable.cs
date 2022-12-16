using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BVPortalApi.Migrations
{
    public partial class AddedHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientTerm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TermText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTerm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientTerm_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientTermHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    OldTermText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldTerm = table.Column<int>(type: "int", nullable: false),
                    NewTermText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewTerm = table.Column<int>(type: "int", nullable: false),
                    ReasonForChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTermHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientTermHistory_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpClientPerHour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    PerHour = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpClientPerHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpClientPerHour_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpClientPerHour_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpClientPerHourHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    OldPerHour = table.Column<float>(type: "real", nullable: false),
                    NewPerHour = table.Column<float>(type: "real", nullable: false),
                    ReasonForChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpClientPerHourHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpClientPerHourHistory_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpClientPerHourHistory_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientTerm_ClientId",
                table: "ClientTerm",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTermHistory_ClientId",
                table: "ClientTermHistory",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpClientPerHour_ClientId",
                table: "EmpClientPerHour",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpClientPerHour_EmployeeId",
                table: "EmpClientPerHour",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpClientPerHourHistory_ClientId",
                table: "EmpClientPerHourHistory",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpClientPerHourHistory_EmployeeId",
                table: "EmpClientPerHourHistory",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTerm");

            migrationBuilder.DropTable(
                name: "ClientTermHistory");

            migrationBuilder.DropTable(
                name: "EmpClientPerHour");

            migrationBuilder.DropTable(
                name: "EmpClientPerHourHistory");
        }
    }
}
