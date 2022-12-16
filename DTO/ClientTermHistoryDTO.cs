using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.DTO
{
    public class ClientTermHistoryDTO
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string? OldTermText { get; set; }
        public int  OldTerm { get; set; }
        public string? NewTermText { get; set; }
        public int NewTerm { get; set; }
        public string? ReasonForChange { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? ChangeBy { get; set; }
        
    }
}