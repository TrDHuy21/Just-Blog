 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CategoryDtos;
using Application.Dto.CommentDtos;
using Application.Dto.PostDtos;
using Application.Dto.UserDtos;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping
{
    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<Post, IndexPostDto>()
                .ForMember(d => d.Rate, x => x.MapFrom(e => e.PostRates.Any() ? e.PostRates.Average(ps => ps.Point) : 0))
                .ForMember(d => d.Author, x => x.MapFrom(e => new AuthorDto { 
                    Id = e.CreatedByUser.Id,
                    FullName = e.CreatedByUser.FullName
                }));


            CreateMap<Post, DetailPostDto>()
                .ForMember(dto => dto.TagNames, x => x.MapFrom(e => e.Tags.Select(tag => tag.TagName)))
                .ForMember(dto => dto.Rate, x => x.MapFrom(e => e.PostRates.Average(ps => ps.Point)))
                .ForMember(dto => dto.CategoryDto, x => x.MapFrom(e => new IndexCategoryDto
                {
                    Id = e.Category.Id,
                    CategoryName = e.Category.CategoryName,
                    Description = e.Category.Description
                }))
                .ForMember(dto => dto.Comments, x => x.MapFrom(e => e.Comments.Select(c => new IndexCommentDto
                {
                    Content = c.Content,
                    CreatedDate = c.CreatedDate,
                    UserName = c.CreatedByUser.UserName,
                    UpdatedDate = c.UpdatedDate
                }).OrderBy(c => c.CreatedDate)
                ));

            CreateMap<CreatePostDto, Post>()
                .ForMember(e => e.CategoryId, x => x.MapFrom(dto => dto.CategoryId));

            CreateMap<UpdatePostDto, Post>()
                .ForMember(e => e.CategoryId, x => x.MapFrom(dto => dto.CategoryId));

            CreateMap<Post, UpdatePostDto>()
                .ForMember(dto => dto.CategoryId, x => x.MapFrom(e => e.CategoryId))
                .ForMember(dto => dto.TagNames, x => x.MapFrom(e => string.Join(" ", e.Tags.Select(tag => tag.TagName))));

        }
    }
}
