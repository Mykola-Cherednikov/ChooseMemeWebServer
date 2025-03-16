using ChooseMemeWebServer.API;
using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Services;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Infrastructure;
using ChooseMemeWebServer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddDbContext<IDbContext, NpgsqlContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DBContext"), npgoptions => npgoptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddScoped<IHelperService, HelperService>();
            builder.Services.AddScoped<ILobbyService, LobbyService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IPresetService, PresetService>();
            builder.Services.AddScoped<IServerService, ServerService>();

            builder.Services.AddScoped<IFeatureService, FeatureService>();
            builder.Services.AddScoped<IWebSocketSenderService, WebSocketSenderService>();
            builder.Services.AddScoped<IWebSocketConnectionService, WebSocketConnectionService>();
            builder.Services.AddScoped<IWebSocketRequestService, WebSocketRequestService>();

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
