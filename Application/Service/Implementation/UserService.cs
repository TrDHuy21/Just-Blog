using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDtos;
using Application.Service.Interface;
using AutoMapper;
using Infrastructure.UnitOfWork;

namespace Application.Service.Implementation
{
    public class UserService : IUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            await _unitOfWork.Roles.GetByIdAsync(user.RoleId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
