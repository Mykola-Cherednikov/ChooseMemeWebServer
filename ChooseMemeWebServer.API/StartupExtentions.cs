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

                var lobby = lobbyService.CreateLobby();
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
            }
        }
    }
}
