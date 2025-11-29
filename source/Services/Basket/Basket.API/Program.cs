namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Services
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
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
                .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"))
                .AddRedis(builder.Configuration.GetConnectionString("RedisConnection"));
            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
            {
                opt.Address = new Uri(builder.Configuration["gRPCSetting:DiscountUrl"]);
            });
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
