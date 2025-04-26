using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Service.Interface;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Implementation
{
    public class BaseEntityservice<T> : IBaseEntityService<T> where T : BaseEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseEntityservice(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Add(T t)
        {
            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity
                           ?? throw new Exception("jwt is invalid");
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? throw new Exception("User ID not found in token");
            // Add your logic here
            t.CreatedBy = Convert.ToInt16(userId);
            t.CreatedDate = DateTime.Now;
        }

        public void Update(T t)
        {
            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity
                           ?? throw new Exception("jwt is invalid");
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? throw new Exception("User ID not found in token");
            // Add your logic here
            t.UpdatedBy = Convert.ToInt16(userId);
            t.UpdatedDate = DateTime.Now;
        }
    }
}
