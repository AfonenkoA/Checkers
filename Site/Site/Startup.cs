using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Checkers.Data.Repository.Interface;
using Checkers.Data.Repository.MSSqlImplementation;
using Checkers.Data.Entity;

namespace Checkers.Site
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddMvc();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IForumRepository, ForumRepository>();
            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
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
