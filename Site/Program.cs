using Api.Interface;
using Api.WebImplementation;
using Site.Repository.Implementation;
using Site.Repository.Interface;
using static Microsoft.AspNetCore.Builder.WebApplication;
using static Microsoft.AspNetCore.WebHost;

namespace Site;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddHttpContextAccessor()
            .AddSingleton<IAsyncUserApi, UserWebApi>()
            .AddSingleton<IAsyncResourceService, AsyncResourceWebApi>()
            .AddSingleton<IAsyncStatisticsApi,StatisticsWebApi>()
            .AddSingleton<IAsyncChatApi, ChatWebApi>()
            .AddSingleton<IAsyncNewsApi,NewsWebApi>()
            .AddSingleton<IAsyncForumApi, ForumWebApi>()
            .AddSingleton<IAsyncItemApi,ItemWebApi>()
            .AddSingleton<IResourceRepository,ResourceRepository>()
            .AddSingleton<IItemRepository,ItemRepository>()
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<INewsRepository,NewsRepository>()
            .AddSingleton<IChatRepository,ChatRepository>()
            .AddSingleton<IForumRepository,ForumRepository>();

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

        app.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}");

        app.Run();
        //CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        CreateDefaultBuilder(args).UseStartup<Startup>();
}