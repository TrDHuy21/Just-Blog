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
    public class UserRepo: GenericRepo<User>, IUserRepo
    {
        public UserRepo(JustBlogContext dbContext) : base(dbContext)
        {

        }

        public Task<User?> GetByIdSqlRawAsync(int id)
        {
            throw new Exception();

        }
    }
}
