using ChooseMemeWebServer.API;
using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Services;
using ChooseMemeWebServer.Infrastructure.Services;
using ChooseMemeWebServer.Infrastructure.TestServices;
using System.Reflection;

namespace ChooseMemeWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ILobbyService, LobbyService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IWebSocketSenderService, WebSocketSenderService>();
            builder.Services.AddScoped<IWebSocketConnectionService, WebSocketConnectionService>();
            builder.Services.AddScoped<IWebSocketRequestService, WebSocketRequestService>();
            builder.Services.AddScoped<IServerService, ServerService>();
            builder.Services.AddScoped<IHelperService, HelperService>();
            //builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IPresetService, PresetTestService>();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
            });

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
