using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Core.Services;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.PlayerJoinLobby
{
    internal class PlayerJoinLobbyHandler : IRequestHandler<PlayerJoinLobbyCommand, PlayerJoinLobbyResponse>
    {
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public PlayerJoinLobbyHandler(ILobbyService lobbyService, IMapper mapper)
        {
            _lobbyService = lobbyService;
            _mapper = mapper;
        }

        public Task<PlayerJoinLobbyResponse> Handle(PlayerJoinLobbyCommand request, CancellationToken cancellationToken)
        {
            PlayerJoinLobbyResponse response = new();
            response.IsSuccess = _lobbyService.TryPlayerJoinToLobby(request.LobbyCode, request.Player, out var lobby);

            response.Lobby = lobby;
            return Task.FromResult(response);
        }
    }
}
