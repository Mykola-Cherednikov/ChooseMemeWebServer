using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands
{
    public class AddBotToLobbyCommand : IRequest<AddBotToLobbyResponse>
    {
        public string Code { get; set; } = string.Empty;
    }

    public class AddBotToLobbyHandler : IRequestHandler<AddBotToLobbyCommand, AddBotToLobbyResponse>
    {
        private readonly ILobbyService _lobbyService;
        private readonly IMapper _mapper;

        public AddBotToLobbyHandler(ILobbyService lobbyService, IMapper mapper)
        {
            _lobbyService = lobbyService;
            _mapper = mapper;
        }

        public async Task<AddBotToLobbyResponse> Handle(AddBotToLobbyCommand request, CancellationToken cancellationToken)
        {
            AddBotToLobbyResponse response = new();
            var lobby = await _lobbyService.BotJoinToLobby(request.Code);

            response.Lobby = _mapper.Map<LobbyDTO>(lobby);

            return response;
        }
    }

    public class AddBotToLobbyResponse
    {
        public LobbyDTO Lobby { get; set; } = null!;
    }
}
