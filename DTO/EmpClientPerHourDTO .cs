using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class EmpClientPerHourDTO
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public float PerHour { get; set; }
        public string Employee { get; set; }
        public string Client { get; set; }
    }
}