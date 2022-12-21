using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public int Term { get; set; }
        public string Status { get; set; }
    }
}