using ChooseMemeWebServer.API;
using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Services;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Infrastructure;
using ChooseMemeWebServer.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "your_issuer",
                    ValidAudience = "your_audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
                };
            });

            var app = builder.Build();

            app.UseWebSockets();
            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            var presetsFolder = builder.Configuration.GetSection("PresetsPath").Value ?? "C:/Presets";
            if (!Directory.Exists(presetsFolder))
            {
                Directory.CreateDirectory(presetsFolder);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(presetsFolder),
                RequestPath = "/Presets"
            });

            app.InitData().GetAwaiter().GetResult();
            app.Run();
        }
    }
}
