using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        // Navigation properties
      
        // user - role: many to one
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // user - postrate: one to many
        public virtual ICollection<PostRate> PostRates { get; set; }

        // user - Category: one to many
        public virtual ICollection<Category> CreatedCategories { get; set; }
        public virtual ICollection<Category> UpdatedCategories { get; set; }

        // user - post: one to many
        public virtual ICollection<Post> CreatedPosts { get; set; }
        public virtual ICollection<Post> UpdatedPosts { get; set; }

        // user - comment: one to many
        public virtual ICollection<Comment> CreatedComments { get; set; }
        public virtual ICollection<Comment> UpdatedComments { get; set; }

        // user - user: one to many
        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> UpdatedUsers { get; set; }




    }
}
