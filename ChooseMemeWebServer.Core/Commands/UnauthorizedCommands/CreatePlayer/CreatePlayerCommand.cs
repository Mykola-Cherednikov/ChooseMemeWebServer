using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreatePlayer
{
    public class CreatePlayerCommand : IRequest<CreatePlayerResponse>
    {
        public string Username { get; set; } = string.Empty;
    }
}
