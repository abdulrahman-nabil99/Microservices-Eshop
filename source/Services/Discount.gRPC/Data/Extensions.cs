namespace Discount.gRPC.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigrations (this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope ();
            using var contex = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            contex.Database.MigrateAsync();
            return builder;
        }
    }
}
