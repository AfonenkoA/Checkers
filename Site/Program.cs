using Api.Interface;
using Api.WebImplementation;
using Microsoft.AspNetCore;
using Site.Repository;
using Site.Repository.Implementation;
using Site.Repository.Interface;

namespace Site;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddHttpContextAccessor()
            .AddSingleton<IAsyncUserApi, UserWebApi>()
            .AddSingleton<IAsyncResourceService, AsyncResourceWebApi>()
            .AddSingleton<IAsyncStatisticsApi,StatisticsWebApi>()
            .AddSingleton<IAsyncNewsApi,NewsWebApi>()
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<INewsRepository,NewsRepository>();

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
        WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
}