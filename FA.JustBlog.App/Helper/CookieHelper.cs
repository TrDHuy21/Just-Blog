using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Azure.Core;

namespace FA.JustBlog.App.Helper
{
    public class CookieHelper
    {
        private readonly HttpClient _httpClient;

        public CookieHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static List<Claim> GetClaims(HttpRequest request)
        {
            string? token;
            request.Cookies.TryGetValue("Jwt", out token);
            if (token == null)
            {
                return null;
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
           
            return jwtToken.Claims.ToList();
        }

       
    }
}
