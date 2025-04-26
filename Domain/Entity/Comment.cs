using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        // comment - post: many to one
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
