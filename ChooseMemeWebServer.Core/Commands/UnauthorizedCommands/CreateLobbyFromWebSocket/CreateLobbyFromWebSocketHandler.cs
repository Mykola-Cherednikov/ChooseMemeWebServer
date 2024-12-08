using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyFromWebSocket
{
    public class CreateLobbyFromWebSocketHandler : IRequestHandler<CreateLobbyFromWebSocketCommand, CreateLobbyFromWebSocketResponse>
    {
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public CreateLobbyFromWebSocketHandler(ILobbyService lobbyService, IMapper mapper)
        {
            _lobbyService = lobbyService;
            _mapper = mapper;
        }

        public Task<CreateLobbyFromWebSocketResponse> Handle(CreateLobbyFromWebSocketCommand request, CancellationToken cancellationToken)
        {
            CreateLobbyFromWebSocketResponse response = new();
            Lobby lobby = _lobbyService.CreateLobby();
            response.Lobby = lobby;
            return Task.FromResult(response);
        }
    }
}
