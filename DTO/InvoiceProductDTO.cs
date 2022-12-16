using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class InvoiceProductDTO
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ProjectId { get; set; }
        public float PerHourCost { get; set; }
        public int TotalHours { get; set; }
        public float TotalCost { get; set; }
        public string? Employee { get; set; }
        public string? Project { get; set; }
        public string? ProjectType { get; set; }
    }    
}