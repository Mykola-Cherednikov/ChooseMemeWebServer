using ChooseMemeWebServer.Application.Interfaces;

namespace ChooseMemeWebServer.API
{
    public static class StartupExtentions
    {
        public static async Task InitData(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var lobbyService = scope.ServiceProvider.GetRequiredService<ILobbyService>();
                var presetService = scope.ServiceProvider.GetRequiredService<IPresetService>();

                var preset = await presetService.CreatePreset("First");
                var lobby = await lobbyService.CreateLobby(preset.Id);
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
            }
        }
    }
}
