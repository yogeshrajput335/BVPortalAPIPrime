using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BVPortalApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Term = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Openjobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Openjobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentOptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AssetType",
                        principalColumn: "Id");
                });

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
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerAddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteToCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetPaidNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "EmployeeBasicInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalEmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMarried = table.Column<bool>(type: "bit", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBothAddressSame = table.Column<bool>(type: "bit", nullable: false),
                    CurrentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBasicInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBasicInfo_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PersonalEmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContact_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Leave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leave_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Leave_LeaveType_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferBy = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    Technology = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Employee_ReferBy",
                        column: x => x.ReferBy,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidates_Openjobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Openjobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetAllocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    AllocatedById = table.Column<int>(type: "int", nullable: true),
                    AllocatedToId = table.Column<int>(type: "int", nullable: true),
                    AllocatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAllocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetAllocation_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetAllocation_Employee_AllocatedById",
                        column: x => x.AllocatedById,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetAllocation_Employee_AllocatedToId",
                        column: x => x.AllocatedToId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAssignment_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectAssignment_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Timesheet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    WeekEndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheet_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Timesheet_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ItemTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    IsProduct = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceProduct_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceProduct_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceProduct_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceProduct_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimesheetId = table.Column<int>(type: "int", nullable: true),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimesheetApproval_Employee_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimesheetApproval_Timesheet_TimesheetId",
                        column: x => x.TimesheetId,
                        principalTable: "Timesheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimesheetDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimesheetId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    WorkDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hours = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimesheetDetail_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimesheetDetail_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimesheetDetail_Timesheet_TimesheetId",
                        column: x => x.TimesheetId,
                        principalTable: "Timesheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetAllocation_AllocatedById",
                table: "AssetAllocation",
                column: "AllocatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAllocation_AllocatedToId",
                table: "AssetAllocation",
                column: "AllocatedToId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAllocation_AssetId",
                table: "AssetAllocation",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TypeId",
                table: "Assets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobId",
                table: "Candidates",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ReferBy",
                table: "Candidates",
                column: "ReferBy");

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

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBasicInfo_EmployeeId",
                table: "EmployeeBasicInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContact_EmployeeId",
                table: "EmployeeContact",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ClientId",
                table: "Invoice",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CompanyId",
                table: "Invoice",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_EmployeeId",
                table: "InvoiceProduct",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_InvoiceId",
                table: "InvoiceProduct",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ProductId",
                table: "InvoiceProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ProjectId",
                table: "InvoiceProduct",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ServiceId",
                table: "InvoiceProduct",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_EmployeeId",
                table: "Leave",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_LeaveTypeId",
                table: "Leave",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientId",
                table: "Project",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignment_EmployeeId",
                table: "ProjectAssignment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignment_ProjectId",
                table: "ProjectAssignment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_EmployeeId",
                table: "Timesheet",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_ProjectId",
                table: "Timesheet",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetApproval_ApproverId",
                table: "TimesheetApproval",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetApproval_TimesheetId",
                table: "TimesheetApproval",
                column: "TimesheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetDetail_EmployeeId",
                table: "TimesheetDetail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetDetail_ProjectId",
                table: "TimesheetDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetDetail_TimesheetId",
                table: "TimesheetDetail",
                column: "TimesheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAllocation");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "ClientTerm");

            migrationBuilder.DropTable(
                name: "ClientTermHistory");

            migrationBuilder.DropTable(
                name: "EmpClientPerHour");

            migrationBuilder.DropTable(
                name: "EmpClientPerHourHistory");

            migrationBuilder.DropTable(
                name: "EmployeeBasicInfo");

            migrationBuilder.DropTable(
                name: "EmployeeContact");

            migrationBuilder.DropTable(
                name: "HolidayMaster");

            migrationBuilder.DropTable(
                name: "InvoiceProduct");

            migrationBuilder.DropTable(
                name: "Leave");

            migrationBuilder.DropTable(
                name: "PaymentOption");

            migrationBuilder.DropTable(
                name: "ProjectAssignment");

            migrationBuilder.DropTable(
                name: "ReferList");

            migrationBuilder.DropTable(
                name: "TimesheetApproval");

            migrationBuilder.DropTable(
                name: "TimesheetDetail");

            migrationBuilder.DropTable(
                name: "TimesheetMaster");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Openjobs");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "LeaveType");

            migrationBuilder.DropTable(
                name: "Timesheet");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
