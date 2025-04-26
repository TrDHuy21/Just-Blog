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
    public class PostRateRepo : GenericRepo<PostRate>, IPostRateRepo
    {
        public PostRateRepo(JustBlogContext context) : base(context)
        {
        }

        public  async Task<PostRate?> GetByPostIdAndUserId(int postId, int userId)
        {
            var postRate = await _context.Set<PostRate>().Where(x => x.PostId == postId && x.UserId == userId).FirstOrDefaultAsync();
            return postRate;
        }
    }
}
