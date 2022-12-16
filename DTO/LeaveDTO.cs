using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class LeaveDTO
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public int? LeaveTypeId { get; set; }
        public string? LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Reason{ get; set; }
        public string? Status { get; set; }
    }
}