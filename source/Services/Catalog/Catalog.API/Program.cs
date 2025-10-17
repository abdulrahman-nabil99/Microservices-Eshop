namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add Services HERE
            builder.Services.AddCarter();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });

            var app = builder.Build();
            app.MapCarter();

            app.Run();
        }
    }
}
