namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // AddCarter
            services.AddCarter();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // UseCarter
            app.MapCarter();
            return app;
        }
    }
}
