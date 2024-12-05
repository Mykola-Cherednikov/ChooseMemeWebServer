using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyWithServer
{
    public class CreateLobbyWithServerHandler : IRequestHandler<CreateLobbyWithServerCommand, CreateLobbyWithServerResponse>
    {
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public CreateLobbyWithServerHandler(ILobbyService lobbyService, IMapper mapper)
        {
            _lobbyService = lobbyService;
            _mapper = mapper;
        }

        public Task<CreateLobbyWithServerResponse> Handle(CreateLobbyWithServerCommand request, CancellationToken cancellationToken)
        {
            CreateLobbyWithServerResponse response = new();
            Lobby lobby = _lobbyService.CreateLobbyWithServer(request.WebSocket);
            response.Lobby = _mapper.Map<LobbyDTO>(lobby);
            return Task.FromResult(response);
        }
    }
}
