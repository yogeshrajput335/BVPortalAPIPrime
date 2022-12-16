using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BVPortalApi.Models;

namespace BVPortalApi.DTO
{
    public class TimesheetDTO
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? ProjectId { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }
        public string? ClientName { get; set; }
        public DateTime WeekEndingDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Duration { get; set; }
        public string? Status { get; set; }
        public List<TimesheetDetail> Detail { get; set; } = null;
    }
    public class TimesheetDataDTO
    {
        public TimesheetDTO timesheet { get; set; }
        public List<DateTime> date { get; set; }
        public List<string> data { get; set; }
    }
}