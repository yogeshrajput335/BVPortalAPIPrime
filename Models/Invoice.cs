using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public string? FromLine1 { get; set; }
        public string? FromLine2 { get; set; }
        public string? FromLine3 { get; set; }
        public string? Term { get; set; }
        public string? Status { get; set; }
        public virtual Client Client { get; set; }
        public ICollection<InvoiceProduct> InvoiceProduct { get; set; }
    }
}