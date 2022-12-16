using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Models
{
    public class BVContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public BVContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("WebApiDatabase");
            options.UseSqlServer(connectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetAllocation> AssetAllocation { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<HolidayMaster> HolidayMaster { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<Openjobs> Openjobs { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ReferList> ReferList { get; set; }
        public DbSet<Timesheet> Timesheet { get; set; }
        public DbSet<TimesheetMaster> TimesheetMaster { get; set; }
        public DbSet<AssetType> AssetType { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeBasicInfo> EmployeeBasicInfo { get; set; }
        public DbSet<EmployeeContact> EmployeeContact { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignment { get; set; }
        public DbSet<TimesheetApproval> TimesheetApproval { get; set; }
        public DbSet<TimesheetDetail> TimesheetDetail { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceProduct> InvoiceProduct { get; set; }
        public DbSet<ClientTerm> ClientTerm { get; set; }
        public DbSet<ClientTermHistory> ClientTermHistory { get; set; }
        public DbSet<EmpClientPerHour> EmpClientPerHour { get; set; }
        public DbSet<EmpClientPerHourHistory> EmpClientPerHourHistory { get; set; }

    }
}