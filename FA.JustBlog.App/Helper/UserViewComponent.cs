using Application.Dto.CategoryDtos;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Application.Dto.UserDtos;

namespace FA.JustBlog.App.Helper
{
    public class UserViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public UserViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claims = CookieHelper.GetClaims(Request);
            if(claims == null)
            {
                return View(null);
            }
            var id = Convert.ToInt16(claims.Find(c => c.Type == "nameid")?.Value);
            var user = await _httpClient.GetFromJsonAsync<UserDto>(CommonUrl.USER_GET_BY_ID(id));
           
            return View(user);
        }
    }
}
