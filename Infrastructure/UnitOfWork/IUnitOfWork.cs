using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repo.Interface;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        IRoleRepo Roles { get; }
        IUserRepo Users { get; }
        ICategoryRepo Categories { get; }
        IPostRepo Posts { get; }
        IPostRateRepo PostRates { get; }
        ICommentRepo Comments { get; }
        ITagRepo Tags { get; }

        Task<int> SaveChange();
            
    }
}
