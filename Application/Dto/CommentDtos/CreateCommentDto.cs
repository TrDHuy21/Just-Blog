using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.CommentDtos
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
