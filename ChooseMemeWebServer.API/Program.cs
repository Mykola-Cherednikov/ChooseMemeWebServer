using ChooseMemeWebServer.API;
using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Services;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Infrastructure;
using ChooseMemeWebServer.Infrastructure.Options;
using ChooseMemeWebServer.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddDbContext<IDbContext, NpgsqlContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("DBContext"),
                npgoptions =>
                {
                    npgoptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }
            ));

            builder.Services.Configure<DataOptions>(builder.Configuration.GetSection(DataOptions.SectionName));
            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.SectionName));

            builder.Services.AddSingleton<IDataService, DataService>();

            builder.Services.AddScoped<IHelperService, HelperService>();
            builder.Services.AddScoped<ILobbyService, LobbyService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IPresetService, PresetService>();
            builder.Services.AddScoped<IServerService, ServerService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IFeatureService, FeatureService>();
            builder.Services.AddScoped<IWebSocketSenderService, WebSocketSenderService>();
            builder.Services.AddScoped<IWebSocketConnectionService, WebSocketConnectionService>();
            builder.Services.AddScoped<IWebSocketRequestService, WebSocketRequestService>();

            builder.Services.AddScoped<IHashService, HashService>();
            builder.Services.AddScoped<ITokenService, JWTTokenService>();

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
                    ValidIssuer = "https://localhost:7185/",
                    ValidAudience = "https://localhost:7185/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetRequiredSection("JWT:SecretKey").Value ?? throw new Exception("Not found JWT key in configuration for authorization startup")))
                };
            });

            var app = builder.Build();

            app.UseWebSockets();
            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            var presetsFolder = builder.Configuration.GetRequiredSection("Data").GetRequiredSection("PresetsPath").Value ?? "C:/Presets";
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
