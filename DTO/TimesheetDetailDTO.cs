using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class TimesheetDetailDTO
    {
        public int Id { get; set; }
        public int? TimesheetId { get; set; }
        public int? ProjectId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? WorkDay { get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }
    }
}