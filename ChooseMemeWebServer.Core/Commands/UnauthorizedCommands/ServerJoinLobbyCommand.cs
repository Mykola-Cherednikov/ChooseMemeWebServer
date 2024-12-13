using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands
{
    public class ServerJoinLobbyCommand : IRequest
    {
        public Lobby Lobby { get; set; } = null!;
    }

    public class ServerJoinLobbyHandler : IRequestHandler<ServerJoinLobbyCommand>
    {
        private readonly ILobbyService _lobbyService;

        public ServerJoinLobbyHandler(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public async Task Handle(ServerJoinLobbyCommand request, CancellationToken cancellationToken)
        {
            await _lobbyService.ServerJoinToLobby(request.Lobby);
        }
    }
}
