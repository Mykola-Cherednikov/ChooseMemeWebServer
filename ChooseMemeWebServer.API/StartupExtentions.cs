using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;

namespace ChooseMemeWebServer.API
{
    public static class StartupExtentions
    {
        public static async Task InitData(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
                var lobbyService = scope.ServiceProvider.GetRequiredService<ILobbyService>();
                var presetService = scope.ServiceProvider.GetRequiredService<IPresetService>();

                var preset = context.Presets.FirstOrDefault();

                if (preset == null)
                {
                    preset = await presetService.CreatePreset("First");
                }

                var lobby = await lobbyService.CreateLobby(preset.Id);
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
            }
        }
    }
}
