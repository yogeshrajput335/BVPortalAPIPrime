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
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int? Term { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddressLine1 { get; set; }
        public string? CompanyAddressLine2 { get; set; }
        public string? CompanyAddressLine3 { get; set; }
        public string? CompanyEmailAddress { get; set; }
        public string? CompanyPhoneNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddressLine1 { get; set; }
        public string? CustomerAddressLine2 { get; set; }
        public string? CustomerAddressLine3 { get; set; }
        public string? NoteToCustomer { get; set; }
        public string? GetPaidNotes { get; set; }
        public string? Status { get; set; }
        public float? Total { get; set; }
        public List<InvoiceProductDTO> Products { get; set; }
        
    }
}