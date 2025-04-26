using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDtos;
using Domain.Entity;

namespace Application.Service.Interface
{
    public interface IAuthService
    {
        Task<string?> GetToken(LoginDto loginDto);
        public bool IsOwner(int? userId);
        public bool IsAdmin();
        public User GetCurrentUser();
    }
}
