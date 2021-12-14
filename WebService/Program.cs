using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebService;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseUrls("http://localhost:5005/");
        webBuilder.UseStartup<Startup>();
    });
