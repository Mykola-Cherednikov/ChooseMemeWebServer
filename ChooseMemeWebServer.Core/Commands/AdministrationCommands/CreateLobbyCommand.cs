using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands
{
    public class CreateLobbyCommand : IRequest<CreateLobbyResponse>
    {

    }

    public class CreateLobbyHandler : IRequestHandler<CreateLobbyCommand, CreateLobbyResponse>
    {
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public CreateLobbyHandler(ILobbyService lobbyService, IMapper mapper)
        {
            _lobbyService = lobbyService;
            _mapper = mapper;
        }

        public Task<CreateLobbyResponse> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            CreateLobbyResponse response = new();
            response.Lobby = _mapper.Map<LobbyDTO>(_lobbyService.CreateLobby());

            return Task.FromResult(response);
        }
    }

    public class CreateLobbyResponse
    {
        public LobbyDTO Lobby { get; set; } = null!;
    }
}
