using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class EmpClientPerHourHistory
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public float OldPerHour { get; set; }
        public float NewPerHour { get; set; }
        public string? ReasonForChange { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeBy { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}