using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.AddBotToLobby
{
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
}
