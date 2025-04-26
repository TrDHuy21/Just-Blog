using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly JustBlogContext _context;

        public GenericRepo(JustBlogContext context)
        {
            _context = context;
        }

        public Task Delete(int id)
        {
            var t = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(t);
            return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public IQueryable<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var t = _context.Set<T>().Where(predicate);
            return t;
        }
    }
}
