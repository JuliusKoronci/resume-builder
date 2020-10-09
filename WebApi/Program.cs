using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Context;

namespace WebApi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (env.IsDevelopment())
                {
                    await context.Database.EnsureDeletedAsync();
                }

                await context.Database.MigrateAsync();

                var seeds = scope.ServiceProvider.GetServices<ISeed>();
                foreach (var seed in seeds)
                {
                    await seed.SeedInitData();
                    if (env.IsDevelopment())
                    {
                        await seed.SeedSampleData();
                    }
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
