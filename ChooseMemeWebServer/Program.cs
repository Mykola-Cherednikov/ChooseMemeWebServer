namespace ChooseMemeWebServer.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            var app = builder.Build();
            app.UseWebSockets();
            app.MapControllers();
            app.Run();
        }
    }
}
