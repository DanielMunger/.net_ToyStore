using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTracker.Models
{
    public class Sale
    {   [Key]
        public int SaleId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public decimal Total { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Toy> Toys { get; set; }
    }
}
