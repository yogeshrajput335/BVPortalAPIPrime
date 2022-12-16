using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmployeeType { get; set; }
        public string? Status { get; set; }
        public User User { get; set; }
        public ICollection<Timesheet> Timesheet { get; set; }
        public ICollection<ProjectAssignment> ProjectAssignment { get; set; }
        public ICollection<TimesheetDetail> TimesheetDetail { get; set; }
        public ICollection<InvoiceProduct> InvoiceProduct { get; set; }
    }
}