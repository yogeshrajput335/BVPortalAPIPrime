using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class EmpClientPerHourHistoryDTO
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public float OldPerHour { get; set; }
        public float NewPerHour { get; set; }
        public string? ReasonForChange { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? ChangeBy { get; set; }
        public string Employee { get; set; }
        public string Client { get; set; }
    }
}