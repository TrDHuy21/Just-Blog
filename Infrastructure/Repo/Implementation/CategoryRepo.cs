using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Context;
using Infrastructure.Repo.Interface;

namespace Infrastructure.Repo.Implementation
{
    public class CategoryRepo: GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(JustBlogContext dbContext) : base(dbContext)
        {
        }
    }
}
