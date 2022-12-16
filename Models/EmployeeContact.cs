using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class EmployeeContact
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WorkEmail { get; set; }
        public string? EmergencyContactName { get; set; }
        public int EmergencyContactNumber { get; set; }
        public virtual Employee Employee { get; set; }
    }
}