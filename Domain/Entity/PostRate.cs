using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class PostRate
    {
        
        public decimal Point { get; set; }

        // Navigation properties
        // postRate - post: many to one
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        // postRate - user: many to one
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
