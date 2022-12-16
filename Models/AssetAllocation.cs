using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class AssetAllocation
    {
        public int Id { get; set; }
        [ForeignKey("Asset")]
        public int? AssetId { get; set; }
        [ForeignKey("Employee")]
        public int? AllocatedById { get; set; }
        [ForeignKey("Employee")]
        public int? AllocatedToId { get; set; }
        public DateTime? AllocatedDate{ get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Status { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual Employee AllocatedTo { get; set; }
        public virtual Employee AllocatedBy { get; set; }
    }
}