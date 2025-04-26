using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.TagDtos;
using Domain.Entity;

namespace Application.Service.Interface
{
    public interface ITagService
    {
        Task AddTag(TagDto tagDto);
        Task AddTag(int Postid, List<string> tagNames);
        Task<List<TagDto>> GetTopPopular(int top);
    }
}
