using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class InvoiceProduct
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Invoice")]
        public int? InvoiceId { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public float PerHourCost { get; set; }
        public int TotalHours { get; set; }
        public float TotalCost { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}