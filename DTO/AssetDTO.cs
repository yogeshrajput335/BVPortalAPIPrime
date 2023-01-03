using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BVPortalApi.Models;

namespace BVPortalApi.DTO
{
    public class AssetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? TypeId { get; set; }
        public string? Type { get; set; }
        public string? ModelNumber { get; set; }
        public string? Status { get; set; }
    }
}