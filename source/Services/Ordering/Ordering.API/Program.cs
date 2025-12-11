using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

namespace Ordering.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // --- Add Services HERE ---
            builder.Services
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices();
            var app = builder.Build();
            // Add Pipeline
            app.UseApiServices();
            if (app.Environment.IsDevelopment())
            {
                await app.InitializeDatabaseAsync();
            }
            app.Run();
        }
    }
}
