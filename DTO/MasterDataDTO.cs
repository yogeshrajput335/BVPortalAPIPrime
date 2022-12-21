using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BVPortalAPIPrime.Models;

namespace BVPortalApi.DTO
{
    public class MasterDataDTO
    {
        public List<Company>? Company { get; set; }
        public List<Customer>? Customer { get; set; }
        public List<Product>? Product { get; set; }
        public List<Service>? Service { get; set; }
        public List<PaymentOption>? PaymentOption { get; set; }
    }
}