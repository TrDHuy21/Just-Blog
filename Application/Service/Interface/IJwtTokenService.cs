using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Service.Interface
{
    public interface IJwtTokenService
    {
        public Task<string?> GenerateToken(User user);
    }
}
