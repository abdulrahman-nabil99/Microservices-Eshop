using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // --- Add Services HERE ---
            builder.Services
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices();
            var app = builder.Build();
            // Add Pipeline
            app = app.UseApiServices();
            app.Run();
        }
    }
}
