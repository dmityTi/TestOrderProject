using System;
using System.Threading.Tasks;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TestOrderProjectWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Get the IWebHost which will host this application.
            var host = CreateWebHostBuilder(args).Build();
            
            //Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<OrderContext>();
                    await OrderContextSeed.SeedAsync(context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Continue to run the application
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}