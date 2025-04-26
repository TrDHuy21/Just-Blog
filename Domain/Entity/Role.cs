using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Role
    {
        public int Id { get; set; } 
        public string RoleName { get; set; }

        // Navigation properties

        // role - user: one to many
        public virtual ICollection<User> Users { get; set; }
    }
}
