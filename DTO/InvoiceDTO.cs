using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? FromLine1 { get; set; }
        public string? FromLine2 { get; set; }
        public string? FromLine3 { get; set; }
        public string? Term { get; set; }
        public string? Status { get; set; }
        public List<InvoiceProductDTO>? Products{ get; set; }
        
    }
}