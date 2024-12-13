using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands
{
    public class ImitateWebSocketCommandCommand : IRequest
    {
        public WebSocketData WebSocketData { get; set; } = null!;

        public string PlayerId { get; set; } = string.Empty;
    }

    public class ImitateWebSocketCommandHandler : IRequestHandler<ImitateWebSocketCommandCommand>
    {
        private readonly IWebSocketCommandService _commandService;
        private readonly IPlayerService _playerService;

        public ImitateWebSocketCommandHandler(IWebSocketCommandService commandService, IPlayerService playerService)
        {
            _commandService = commandService;
            _playerService = playerService;
        }

        public Task Handle(ImitateWebSocketCommandCommand request, CancellationToken cancellationToken)
        {
            Player? player = _playerService.GetOnlinePlayer(request.PlayerId);

            if (player == null)
            {
                return Task.CompletedTask;
            }

            Lobby? lobby = player.Lobby;

            if (lobby == null)
            {
                return Task.CompletedTask;
            }

            _commandService.Handle(request.WebSocketData, player, lobby);

            return Task.CompletedTask;
        }
    }
}
