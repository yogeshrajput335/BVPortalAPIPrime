using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalAPIPrime.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string? Unit { get; set; }
        public float Rate { get; set; }
        public string Status { get; set; }
    }
}