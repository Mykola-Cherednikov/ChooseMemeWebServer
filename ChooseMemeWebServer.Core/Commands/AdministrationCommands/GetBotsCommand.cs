using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands
{
    public class GetBotsCommand : IRequest<GetBotsResponse>
    {

    }

    public class GetBotsHandler(IPlayerService _playerService, IMapper _mapper) : IRequestHandler<GetBotsCommand, GetBotsResponse>
    {
        public Task<GetBotsResponse> Handle(GetBotsCommand request, CancellationToken cancellationToken)
        {
            var list = _playerService.GetOnlineBots();

            return Task.FromResult(new GetBotsResponse() { Bots = _mapper.Map<List<PlayerDTO>>(list) });
        }
    }

    public class GetBotsResponse
    {
        public List<PlayerDTO> Bots { get; set; } = null!;
    }
}
