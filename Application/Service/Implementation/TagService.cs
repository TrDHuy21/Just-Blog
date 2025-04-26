using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.TagDtos;
using Application.Service.Interface;
using AutoMapper;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation
{
    public class TagService : ITagService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddTag(TagDto tagDto)
        {

            var tag = _unitOfWork.Tags.GetTag(tagDto.PostId, tagDto.TagName);
            if (tag != null)
            {
                return;
            }
            tag = _mapper.Map<Tag>(tagDto);
            await _unitOfWork.Tags.AddAsync(tag);
            await _unitOfWork.SaveChange();
        }

        public async Task AddTag(int postId, List<string> tagNames)
        {
            foreach (var tagName in tagNames)
            {
                var tag = _unitOfWork.Tags.GetTag(postId, tagName);
                if (tag != null)
                {
                    continue;
                }
                else
                {
                    await _unitOfWork.Tags.AddAsync(new Tag { TagName = tagName, PostId = postId });
                }
            }
            await _unitOfWork.SaveChange();

        }

        public async Task<List<TagDto>> GetTopPopular(int top)
        {
            var tags = await _unitOfWork.Tags.GetAll()
                .GroupBy(t => t.TagName)
                .Select(g => new
                {
                    TagName = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(t => t.Count)
                .Take(top)
                .Select(e => new TagDto { 
                    TagName = e.TagName
                })
                .ToListAsync();

            return tags;
        }
    }
}
