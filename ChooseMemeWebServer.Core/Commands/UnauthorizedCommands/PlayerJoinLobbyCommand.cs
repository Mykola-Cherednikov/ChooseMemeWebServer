using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands
{
    public class PlayerJoinLobbyCommand : IRequest<PlayerJoinLobbyResponse>
    {
        public string LobbyCode { get; set; } = string.Empty;

        public Player Player { get; set; } = null!;
    }

    public class PlayerJoinLobbyHandler : IRequestHandler<PlayerJoinLobbyCommand, PlayerJoinLobbyResponse>
    {
        private readonly ILobbyService _lobbyService;

        public PlayerJoinLobbyHandler(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public async Task<PlayerJoinLobbyResponse> Handle(PlayerJoinLobbyCommand request, CancellationToken cancellationToken)
        {
            PlayerJoinLobbyResponse response = new();
            var lobby = await _lobbyService.PlayerJoinToLobby(request.LobbyCode, request.Player);

            response.IsSuccess = lobby != null;

            response.Lobby = lobby;

            return response;
        }
    }

    public class PlayerJoinLobbyResponse
    {
        public bool IsSuccess { get; set; }

        public Lobby? Lobby { get; set; } = null!;
    }
}
