using HealthChecks.UI.Client;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Services
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddCarter();
            builder.Services.AddMarten(opt =>
            {
                opt.Connection(builder.Configuration.GetConnectionString("DefaultConnection"));
                opt.Schema.For<ShoppingCart>().Identity(c => c.UserName);
            }).UseLightweightSessions();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"));

            var app = builder.Build();
            // Pipeline
            app.MapCarter();
            app.UseExceptionHandler(config => { });
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.Run();
        }
    }
}
