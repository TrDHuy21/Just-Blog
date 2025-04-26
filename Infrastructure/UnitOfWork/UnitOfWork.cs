using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Entity;
using Infrastructure.Context;
using Infrastructure.Repo.Implementation;
using Infrastructure.Repo.Interface;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly JustBlogContext _context;
        public UnitOfWork(JustBlogContext context) {
            _context = context;
            Roles = new RoleRepo(_context);
            Users = new UserRepo(_context);
            Categories = new CategoryRepo(_context);
            Posts = new PostRepo(_context);
            PostRates = new PostRateRepo(_context);
            Comments = new CommentRepo(_context);
            Tags = new TagRepo(_context);
        }
        public IRoleRepo Roles { get; }
        public IUserRepo Users { get; }
        public ICategoryRepo Categories { get; }
        public IPostRepo Posts { get; }
        public IPostRateRepo PostRates { get; }
        public ICommentRepo Comments { get; }
        public ITagRepo Tags { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChange()
        {
            return  await _context.SaveChangesAsync();
        }
    }
}
