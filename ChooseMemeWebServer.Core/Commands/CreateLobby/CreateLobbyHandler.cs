using ChooseMemeWebServer.Core.Services;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.CreateLobby
{
    public class CreateLobbyHandler : IRequestHandler<CreateLobbyCommand, CreateLobbyResponse>
    {
        private readonly LobbyService _lobbyService;

        public CreateLobbyHandler(LobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public Task<CreateLobbyResponse> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            CreateLobbyResponse response = new();
            response.Success = _lobbyService.TryCreateLobby(request.WebSocket);
            return Task.FromResult(response);
        }
    }
}
