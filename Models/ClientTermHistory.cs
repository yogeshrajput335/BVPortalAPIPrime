using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class ClientTermHistory
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public string OldTermText { get; set; }
        public int OldTerm { get; set; }
        public string NewTermText { get; set; }
        public int NewTerm { get; set; }
        public string? ReasonForChange { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeBy { get; set; }
        public virtual Client Client { get; set; }
    }
}