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
        // [ForeignKey("Product")]
        // public int? ProductId { get; set; }
        // [ForeignKey("Service")]
        // public int? ServiceId { get; set; }
        public string? Product { get; set; }
        public string? Service { get; set; }
        public string? ItemTypeId { get; set; }
        public string? Unit { get; set; }
        public float Quantity { get; set; }
        public float Rate { get; set; }
        public float Total { get; set; }
        public bool IsProduct { get; set; }
        public virtual Invoice Invoice { get; set; }
        // public virtual Product Product { get; set; }
        // public virtual Service Service { get; set; }
    }
}