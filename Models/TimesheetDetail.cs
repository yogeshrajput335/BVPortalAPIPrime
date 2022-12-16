using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class TimesheetDetail
    {
        public int Id { get; set; }
        public int? TimesheetId { get; set; }
        public int? ProjectId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? WorkDay { get; set; }
        public int? Hours { get; set; }
        public Timesheet Timesheet { get; set; }
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}