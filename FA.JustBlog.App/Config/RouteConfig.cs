namespace FA.JustBlog.App.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(IEndpointRouteBuilder routes)
        {
            routes.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            routes.MapControllerRoute(
                name: "FindPost",
                pattern: "Post/{year}/{month}/{title}",
                defaults: new { controller = "Post", action = "FindPost" },
                constraints: new { year = @"\d{4}", month = @"\d{2}" }
            );

        }
    }
}
