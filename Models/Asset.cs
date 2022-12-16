using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey("AssetType")]
        public int? TypeId { get; set; }
        public string? ModelNumber { get; set; }
        public string? Status { get; set; }
        public virtual AssetType AssetType { get; set; }
    }
}