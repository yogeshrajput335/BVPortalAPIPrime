using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? ProjectName{ get; set; }
        public int? ClientId{ get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ProjectType { get; set; }
        public string? Status { get; set; }
        public virtual Client Client { get; set; }
        public ICollection<Timesheet> Timesheet { get; set; }
        public ICollection<ProjectAssignment> ProjectAssignment { get; set; }
        public ICollection<TimesheetDetail> TimesheetDetail { get; set; }
        public ICollection<InvoiceProduct> InvoiceProduct { get; set; }
    }
}