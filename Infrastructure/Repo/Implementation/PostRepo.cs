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
    public class PostRepo: GenericRepo<Post>, IPostRepo
    {
        public PostRepo(JustBlogContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Post> GetAll()
        {
            return _context.Posts;
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Tags)
                //.Include(p => p.Comments)
                .Include(p => p.Category)
                //.Include(p => p.PostRates)
                //.Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
