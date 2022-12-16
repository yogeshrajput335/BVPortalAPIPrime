using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class AssetAllocationDTO
    {
        public int Id { get; set; }
        public int? AssetId { get; set; }
        public string? AssetName { get; set; }
        public int? AllocatedById { get; set; }
        public string? AllocatedBy { get; set; }
        public int? AllocatedToId { get; set; }
        public string? AllocatedTo { get; set; }
        public DateTime? AllocatedDate{ get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Status { get; set; }
    }
}