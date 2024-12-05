using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetLobby
{
    public class GetLobbyHandler : IRequestHandler<GetLobbyCommand, GetLobbyResponse>
    {
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public GetLobbyHandler(ILobbyService lobbyService, IMapper mapper)
        {
            _lobbyService = lobbyService;
            _mapper = mapper;
        }

        public Task<GetLobbyResponse> Handle(GetLobbyCommand request, CancellationToken cancellationToken)
        {
            GetLobbyResponse response = new();

            response.Lobby = _mapper.Map<LobbyDTO>(_lobbyService.GetLobby(request.Code));

            return Task.FromResult(response);
        }
    }
}
