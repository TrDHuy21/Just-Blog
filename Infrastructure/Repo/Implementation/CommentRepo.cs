using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Context;
using Infrastructure.Repo.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo.Implementation
{
    public class CommentRepo : GenericRepo<Comment>, ICommentRepo
    {
        public CommentRepo(JustBlogContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetCommentsByPostId(int postId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.CreatedByUser)
                .ToListAsync();
            return comments;
        }
    }
}
