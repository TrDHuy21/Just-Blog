using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDtos;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping() { 
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
        }
    }
}
