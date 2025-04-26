using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        // navegation properties
        public int? CreatedBy { get; set; }
        public virtual User CreatedByUser { get; set; }

        public int? UpdatedBy { get; set; }
        public virtual User UpdatedByUser { get; set; }

    }
}
