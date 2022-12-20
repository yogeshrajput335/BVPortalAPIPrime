using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalAPIPrime.Models
{
    public class PaymentOption
    {
        public int Id { get; set; }
        public string PaymentOptionName { get; set; }
        public string Status { get; set; }
    }
}