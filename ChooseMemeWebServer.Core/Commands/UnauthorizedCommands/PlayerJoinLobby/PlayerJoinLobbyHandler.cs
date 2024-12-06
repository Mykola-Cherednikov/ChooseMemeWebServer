using AutoMapper;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.PlayerJoinLobby
{
    internal class PlayerJoinLobbyHandler : IRequestHandler<PlayerJoinLobbyCommand, PlayerJoinLobbyResponse>
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
}
