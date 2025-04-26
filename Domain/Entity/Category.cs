using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }


        // Navigation properties

        // category - post: one to many 
        public ICollection<Post> Posts { get; set; }

    }
}
