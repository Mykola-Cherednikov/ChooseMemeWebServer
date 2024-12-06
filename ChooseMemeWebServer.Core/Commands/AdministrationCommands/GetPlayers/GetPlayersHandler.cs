using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetPlayers
{
    public class GetPlayersHandler : IRequestHandler<GetPlayersCommand, GetPlayersResponse>
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public GetPlayersHandler(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        public Task<GetPlayersResponse> Handle(GetPlayersCommand request, CancellationToken cancellationToken)
        {
            var list = _playerService.GetOnlinePlayers();

            return Task.FromResult(new GetPlayersResponse() { Players = _mapper.Map<List<PlayerDTO>>(list) });
        }
    }
}
