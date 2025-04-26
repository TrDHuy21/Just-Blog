using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.TagDtos;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping
{
    public class TagMapping : Profile
    {
        public TagMapping() { 
            CreateMap<Tag, TagDto>().ReverseMap();
                
        }
    }
}
