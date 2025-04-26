using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CommentDtos;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {
            CreateMap<Comment, IndexCommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.CreatedByUser.UserName));

            CreateMap<CreateCommentDto, Comment>();
        }
    }
}
