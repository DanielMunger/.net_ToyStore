using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTracker.Models
{
    public class Comment
    {   [Key]
        public int CommentId { get; set; }
        public string Body { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
