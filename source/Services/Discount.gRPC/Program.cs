namespace Discount.gRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddDbContext<DiscountContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseMigrations();
            app.MapGrpcService<DiscountService>();
            app.Run();
        }
    }
}