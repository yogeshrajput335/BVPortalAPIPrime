using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class EmployeeBasicInfo
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? BloodGroup { get; set; }
        public string? PersonalEmailId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsMarried { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string? PermanentAddress { get; set; }
        public bool IsBothAddressSame { get; set; }
        public string? CurrentAddress { get; set; }
        public string? Gender { get; set; }
        public virtual Employee Employee { get; set; }
    }
}