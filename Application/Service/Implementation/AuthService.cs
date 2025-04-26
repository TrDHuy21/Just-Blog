using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDtos;
using Application.Service.Interface;
using Domain.Entity;
using Infrastructure.Context;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation
{
    public class AuthService : IAuthService
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> GetToken(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName)
                .Include(u => u.Role).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Tên đăng nhập không tồn tại");
            }
            if (user.Password != loginDto.Password) 
            {
                throw new Exception("Sai mật khẩu");
            }

            var token = await _tokenService.GenerateToken(user);
            return token;
        }

        public bool IsAdmin()
        {

            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity
                             ?? throw new Exception("jwt is invalid");
            var role = identity.FindFirst(ClaimTypes.Role)?.Value
                         ?? throw new Exception("Role not found in token");
            if (role == "Admin")
            {
                return true;
            }
            return false;
        }

        public bool IsOwner(int? userId)
        {
            if (userId == null)
            {
                return false;
            }
            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity
                             ?? throw new Exception("jwt is invalid");
            var role = identity.FindFirst(ClaimTypes.Role)?.Value
                         ?? throw new Exception("Role not found in token");
            var currentUserId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                ?? throw new Exception("User ID not found in token");
            return userId.ToString() == currentUserId;
        }

        public User GetCurrentUser()
        {
            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity
                  ?? throw new Exception("jwt is invalid");
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? throw new Exception("User ID not found in token");
            var user = _unitOfWork.Users.GetByIdAsync(Convert.ToInt32(userId)).Result;
            return user;
        }
    }
}
