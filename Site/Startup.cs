using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Checkers.Data.Old;
using Checkers.Data.Repository.MSSqlImplementation;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Site;
using Checkers.Site.Data.Interfaces;
using Checkers.Site.Data.Mocks;

namespace Checkers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.Configure<CookiePolicyOptions>(Options =>
            {
                Options.CheckConsentNeeded = context => true;
                Options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            /*services.AddDbContext<UserContex>(Options => Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAsyncUserApi, UserWebApi>();
            services.AddControllersWithViews();*/

            var config = Configuration.GetSection("DatabaseConfig").Get<DatabaseConfig>();
            services.AddSingleton(new RepositoryFactory(config.Current));
            services.AddSingleton(new GameDatabase.Factory(config.Old));
            services.AddControllers();
            services.AddMvc();
            services.AddSingleton<IUserRep, UserRep>();
            services.AddSingleton<IChatRep, ChatRep>();
            services.AddSingleton<IForumRep, ForumRep>();
            services.AddSingleton<IStatisticsRep, StatisticsRep>();
            services.AddSingleton<IResourceRep, ResourceRep>();
            services.AddSingleton<IItemRep, ItemRep>();
            services.AddSingleton<INewsRep, NewsRep>();
            services.AddHttpContextAccessor();

        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
  
        }
    }
}
