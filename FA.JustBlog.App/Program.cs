using FA.JustBlog.App.Config;

namespace FA.JustBlog.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Thêm route cho bài viết
            app.MapControllerRoute(
                name: "Post",
                pattern: "post/{year:int}/{month:int}/{title}",
                defaults: new { controller = "Post", action = "FindPost" }
            );
            app.MapControllerRoute(
                name: "IndexPostPage",
                pattern: "post/page/{index}",
                defaults: new { controller = "Post", action = "IndexPostPage" }
            );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
