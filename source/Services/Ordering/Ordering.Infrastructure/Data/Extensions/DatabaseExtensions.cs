using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(context);
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedCustomersAsync(context);
            await SeedProductsAsync(context);
            await SeedOrdersAsync(context);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCustomersAsync(ApplicationDbContext context)
        {
            if (await context.Customers.AnyAsync()) return;
            await context.Customers.AddRangeAsync(InitialData.Customers);
            //await context.SaveChangesAsync();
        }
        private static async Task SeedProductsAsync(ApplicationDbContext context)
        {
            if (await context.Products.AnyAsync()) return;
            await context.Products.AddRangeAsync(InitialData.Products);
            //await context.SaveChangesAsync();
        }
        private static async Task SeedOrdersAsync(ApplicationDbContext context)
        {
            if (await context.Orders.AnyAsync()) return;
            await context.Orders.AddRangeAsync(InitialData.Orders);
            //await context.SaveChangesAsync();
        }
    }
}
