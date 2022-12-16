using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class Timesheet
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime WeekEndingDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Duration { get; set; }
        public string? Status { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
        public ICollection<TimesheetDetail> TimesheetDetail { get; set; }
    }
}