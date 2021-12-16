using Api.Interface;
using Api.WebImplementation;
using Site.Repository;

namespace Site;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) 
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();
        app.UseStaticFiles();
        //app.UseMvcWithDefaultRoute();
  
    }
}