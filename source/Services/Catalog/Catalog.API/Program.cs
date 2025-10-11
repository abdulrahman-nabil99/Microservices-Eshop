namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Services HERE

            var app = builder.Build();


            app.Run();
        }
    }
}
