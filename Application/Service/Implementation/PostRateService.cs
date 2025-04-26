using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Service.Interface;
using Domain.Entity;
using Infrastructure.UnitOfWork;

namespace Application.Service.Implementation
{
    public class PostRateService : IPostRateService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAuthService _authService;

        public async Task<bool> RatePost(int postId, decimal rate)
        {
            var user = _authService.GetCurrentUser();
            var postRate = await _unitOfWork.PostRates.GetByPostIdAndUserId(postId, user.Id);
            if (postRate != null)
            {
                postRate.Point = rate;
                await _unitOfWork.PostRates.Update(postRate);
            }
            else
            {
                postRate = new PostRate
                {
                    PostId = postId,
                    UserId = user.Id,
                    Point = rate
                };
                await _unitOfWork.PostRates.AddAsync(postRate);
            }
            var result = await _unitOfWork.SaveChange();
            return result >= 1;
        }
    }
}
