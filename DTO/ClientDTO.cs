using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? ContactPerson { get; set; }
        public string?  Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public int? Term { get; set; }
        public string? TermText { get; set; }
        
    }
    public class SetTermDTO{
        public string? ReasonForChange { get; set; }
        public int? ChangeBy { get; set; }
    }
}