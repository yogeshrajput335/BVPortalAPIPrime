using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class EmployeeContactDTO
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WorkEmail { get; set; }
        public string? EmergencyContactName { get; set; }
        public int EmergencyContactNumber { get; set; }
    }
}