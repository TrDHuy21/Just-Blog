using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Tag
    {
        public string TagName { get; set; }

        // Navigation properties

        // tag - post: many to one
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
