using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public interface IGenericRepo<T> where T : class
    {
        // getall
        // getbyid
        // add
        // update
        // delete
        public Task<T?> GetByIdAsync(int id);
        public Task<IEnumerable<T>?> GetAllAsync();
        public Task AddAsync(T entity);
        public Task Update(T entity);
        public Task Delete(int id);

        public IQueryable<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    }
}
