using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public  class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int Views { get; set; }
        public bool IsPublished { get; set; }

        // Navigation properties

        // post - tag: one to many
        public virtual ICollection<Tag> Tags { get; set; }
        //Post - PostRate: one to many
        public virtual ICollection<PostRate> PostRates { get; set; }

        // Post - Comment: one to many
        public virtual ICollection<Comment> Comments { get; set; }


        // post - category: many to one
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
