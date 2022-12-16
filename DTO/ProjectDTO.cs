using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ProjectType { get; set; }
        public string? Status { get; set; }
    }
}