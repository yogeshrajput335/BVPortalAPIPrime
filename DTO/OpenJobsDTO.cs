using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class OpenjobsDTO
    {
        public int Id { get; set; }
        public string? JobName { get; set; }
        public string? Profile { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public string? Country { get; set; }
        public string? Status { get; set; }
    }
}