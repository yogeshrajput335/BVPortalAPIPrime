using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BVPortalApi.Models
{
    public class ClientTerm
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public string TermText { get; set; }
        public int Term { get; set; }
        public virtual Client Client { get; set; }
    }
}