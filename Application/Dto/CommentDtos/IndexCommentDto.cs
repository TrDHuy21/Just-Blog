using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Dto.CommentDtos
{
    public class IndexCommentDto
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
