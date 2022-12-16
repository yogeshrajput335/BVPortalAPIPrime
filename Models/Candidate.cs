using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName{ get; set; }
        public string? PhoneNo{ get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        [ForeignKey("Employee")]
        public int? ReferBy { get; set; }
        public virtual Employee Employee { get; set; }
         [ForeignKey("Openjobs")]
        public int? JobId { get; set; }
        public virtual Openjobs Openjobs { get; set; }
        public string? Technology{ get; set; }
        public string? Visa{ get; set; }
        public string? Rate{ get; set; }
        public string? Client{ get; set; }
        public string? ClientContact{ get; set; }
        public string? ClientMail{ get; set; }
        public string? Vendor{ get; set; }
        public string? VendorContact { get; set; }
        public string? VendorMail{ get; set; }
        public DateTime? CreatedDate{ get; set; }
    }
}