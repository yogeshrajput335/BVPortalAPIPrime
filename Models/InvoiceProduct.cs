using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BVPortalAPIPrime.Models;

namespace BVPortalApi.Models
{
    public class InvoiceProduct
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Invoice")]
        public int? InvoiceId { get; set; }
        public string? Product { get; set; }
        public string? Service { get; set; }
        public string? ItemTypeId { get; set; }
        public string? Unit { get; set; }
        public float Quantity { get; set; }
        public float Rate { get; set; }
        public float Total { get; set; }
        public bool IsProduct { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}