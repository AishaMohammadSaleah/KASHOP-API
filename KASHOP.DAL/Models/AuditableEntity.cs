using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public class AuditableEntity
    {
        public string Id { get; set; }
        public string createdById { get; set; }
        public DateTime createdAt { get; set; }
        public string? updatedById { get; set; }
        public DateTime? updatedAt { get; set; }
        public ApplicationUser createdBy { get; set; }
        public ApplicationUser? updatedBy { get; set; }

    }
}
