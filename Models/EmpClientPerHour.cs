using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class EmpClientPerHour
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public float PerHour { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}