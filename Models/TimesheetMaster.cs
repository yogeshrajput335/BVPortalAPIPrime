using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class TimesheetMaster
    {
        public int Id { get; set; }
        public string? Year { get; set; }
        public string? Month { get; set; }
        public string? Status { get; set; }
    }
}