using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class TimesheetApprovalDTO
    {
        public int Id { get; set; }
        public int? TimesheetId { get; set; }
        public int? ApproverId { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
    }
}