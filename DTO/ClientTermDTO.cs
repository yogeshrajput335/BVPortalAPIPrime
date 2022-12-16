using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class ClientTermDTO
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string? TermText { get; set; }
        public int Term { get; set; }
    }
}