
namespace FA.JustBlog.App.Config
{
    public class CommonUrl
    {
        public static string BASE_URL = "http://localhost:5088";

        //post url
        public static string POST_GET_ALL_FOR_USER = BASE_URL + "/api/post";
        public static string POST_GET_BY_ID(int id) => BASE_URL + $"/api/post/{id}";
        public static string POST_GET_BY_TAG(string tagName) => BASE_URL + $"/api/post/tag/{tagName}";
        public static string POST_GET_BY_CATEGORY(int id) => BASE_URL + $"/api/post/category/{id}";
        
        public static string? POST_GET_BY_USER = BASE_URL + "/api/post/mypost";
        public static string POST_FIND(int year, int month, string title) => BASE_URL + $"/api/post/{year}/{month}/{title}";
        public static string POST_GET_BY_PAGE(int index) => BASE_URL + $"/api/post/page/{index}";
        public static string? POST_FILTER(string sapxep, string ispublish, int index, int pageSize) => BASE_URL + $"/api/post/filter/{index}?sapxep={sapxep}&ispublish={ispublish}";

        public static string POST_GET_TOP_VIEW(int top) => BASE_URL + $"/api/post/topview/{top}";
        //post admin
        public static string POST_ADMIN = BASE_URL + "/api/AdminPost";
        public static string POST_GET_FOR_UPDATE(int id) => POST_ADMIN + $"/{id}";
        public static string POST_PUBLISH(int id) => POST_ADMIN + $"/PublishPost/{id}";

        //category url
        public static string CATEGORY_GET_ALL = BASE_URL + "/api/category";

        //category admin
        public static string CATEGORY_ADMIN = BASE_URL + "/api/AdminCategory";
        public static string CATEGORY_GET_FOR_UPDATE(int id) => CATEGORY_ADMIN + $"/{id}";

        //login
        public static string LOGIN_URL_GET_TOKEN = BASE_URL + "/api/Auth/login";

        //user
        public static string USER_GET_BY_ID(int id) => BASE_URL + $"/api/User/{id}";

        //tag
        public static string TAG_GET_TOP(int top) => BASE_URL + $"/api/tag/toppopular/{top}";

       

        //comment
        public static string COMMENT_URL = BASE_URL + $"/api/comment";
    }
}
