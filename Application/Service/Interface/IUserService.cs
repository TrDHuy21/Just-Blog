using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDtos;

namespace Application.Service.Interface
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
    }
}
