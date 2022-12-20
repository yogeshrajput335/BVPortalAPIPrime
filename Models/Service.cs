using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalAPIPrime.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string? Unit { get; set; }
        public float Rate { get; set; }
        public string Status { get; set; }
    }
}