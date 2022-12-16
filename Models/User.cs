using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}