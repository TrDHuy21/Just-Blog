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
    public class TagRepo : GenericRepo<Tag>, ITagRepo
    {
        public TagRepo(JustBlogContext context) : base(context)
        {
        }

        public IQueryable<Tag> GetAll()
        {
            return _context.Tags;
        }

        public Tag? GetTag(int Postid, string tagName)
        {
            var tag = _context.Tags.Where(t => t.PostId == Postid && t.TagName == tagName).FirstOrDefault();
            return tag;
        }
    }
}
