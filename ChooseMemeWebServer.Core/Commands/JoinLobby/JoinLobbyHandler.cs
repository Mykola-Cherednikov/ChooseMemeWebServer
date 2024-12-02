using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Core.Services;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.JoinLobby
{
    internal class JoinLobbyHandler : IRequestHandler<JoinLobbyCommand, JoinLobbyResponse>
    {
        private readonly ILobbyService _lobbyService;

        public JoinLobbyHandler(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public Task<JoinLobbyResponse> Handle(JoinLobbyCommand request, CancellationToken cancellationToken)
        {
            JoinLobbyResponse response = new();
            response.Lobby = _lobbyService.JoinToLobby(request.LobbyCode, request.Player);
            return Task.FromResult(response);
        }
    }
}
