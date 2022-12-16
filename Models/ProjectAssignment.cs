using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class ProjectAssignment
    {
        public int Id { get; set; }
        public int? ProjectId{ get; set; }
        public int? EmployeeId{ get; set; }
        public string? Notes { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }
    }
}