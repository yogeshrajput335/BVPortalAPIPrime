using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class TimesheetMasterDTO
    {
        public int Id { get; set; }
        public string? Year { get; set; }
        public string? Month { get; set; }
        public string? Status { get; set; }
    }
}