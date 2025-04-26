namespace FA.JustBlog.Mvc.Config
{
    public class CommonUrl
    {
        public static string BASE_URL = "http://localhost:5088";

        //post url
        public static string POST_URL_GET_ALL = BASE_URL + "/api/post";
        public static string POST_URL_GET_DETAIL(int id) => BASE_URL + $"/api/post/{id}" ;

        //login
        public static string LOGIN_URL_GET_TOKEN = BASE_URL + "/api/Auth/login";
    }
}
