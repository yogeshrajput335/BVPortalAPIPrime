using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Reason{ get; set; }
        public string? Status { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public virtual Employee Employee { get; set; }
    }
}