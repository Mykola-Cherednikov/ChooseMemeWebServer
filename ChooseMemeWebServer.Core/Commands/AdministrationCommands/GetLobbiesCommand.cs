using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands
{
    public class GetLobbiesCommand : IRequest<GetLobbiesResponse>
    {
    }

    public class GetLobbiesHandler : IRequestHandler<GetLobbiesCommand, GetLobbiesResponse>
    {
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public GetLobbiesHandler(ILobbyService lobbyService, IMapper mapper)
        {
            _lobbyService = lobbyService;
            _mapper = mapper;
        }

        public Task<GetLobbiesResponse> Handle(GetLobbiesCommand request, CancellationToken cancellationToken)
        {
            GetLobbiesResponse response = new GetLobbiesResponse();
            response.Lobbies = _mapper.Map<List<LobbyDTO>>(_lobbyService.GetLobbies());
            return Task.FromResult(response);
        }
    }

    public class GetLobbiesResponse
    {
        public List<LobbyDTO> Lobbies { get; set; } = null!;
    }
}
