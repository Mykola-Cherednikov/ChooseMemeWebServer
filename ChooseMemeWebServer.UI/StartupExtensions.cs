using ChooseMemeWebServer.Core.Commands.AdministrationCommands;
using MediatR;

namespace ChooseMemeWebServer.UI
{
    public static class StartupExtensions
    {
        public static async Task InitData(this WebApplication webApplication)
        {
            using(var scope = webApplication.Services.CreateScope())
            {
                var mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();

                var lobby = (await mediatr.Send(new CreateLobbyCommand())).Lobby;
                await mediatr.Send(new AddBotToLobbyCommand() { Code = lobby.Code });
                await mediatr.Send(new AddBotToLobbyCommand() { Code = lobby.Code });
                await mediatr.Send(new AddBotToLobbyCommand() { Code = lobby.Code });
            }
        }
    }
}
