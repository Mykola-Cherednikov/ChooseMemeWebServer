using ChooseMemeWebServer.Core.Common.Mappings;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Core.Services;
using ChooseMemeWebServer.Domain.Models;
using ChooseMemeWebServer.Infrastructure;
using System.Reflection;

namespace ChooseMemeWebServer.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            builder.Services.AddTransient<ILobbyService, LobbyService>();
            builder.Services.AddTransient<IPlayerService, PlayerService>();
            builder.Services.AddTransient<IWebSocketCommandService, WebSocketCommandService>();
            builder.Services.AddTransient<IWebSocketSender, WebSocketSender>();
            builder.Services.AddTransient<IGameService, GameService>();
            builder.Services.AddTransient<WebSocketConnectionManager>();
            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(Lobby).Assembly));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseWebSockets();
            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.InitData().GetAwaiter().GetResult();

            app.Run();
        }
    }
}
