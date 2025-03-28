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
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                var testUser = (await userService.GetAllUsers()).FirstOrDefault(u => u.Username == "TESTBOT");

                if (testUser == null)
                {
                    testUser = await userService.CreateUser("TESTBOT", "TESTBOT");
                }

                var preset = (await presetService.GetPresets()).FirstOrDefault();

                if (preset == null)
                {
                    preset = await presetService.CreatePreset("First", testUser.Id);
                }

                var lobby = await lobbyService.CreateLobby(preset.Id);
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
                await lobbyService.AddBotToLobby(lobby.Code);
            }
        }
    }
}
