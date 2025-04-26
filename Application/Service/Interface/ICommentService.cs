using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CommentDtos;

namespace Application.Service.Interface
{
    public interface ICommentService
    {
        Task<bool> CreateComment(CreateCommentDto createCommentDto);
        Task<bool> DeleteComment(int commentId);
    }
}
