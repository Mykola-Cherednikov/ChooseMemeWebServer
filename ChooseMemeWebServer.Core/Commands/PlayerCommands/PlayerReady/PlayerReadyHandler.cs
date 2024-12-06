using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands.PlayerReady
{
    public class PlayerReadyHandler : IRequestHandler<PlayerReadyCommand>
    {
        private readonly IPlayerService _playerService;

        public PlayerReadyHandler(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Task Handle(PlayerReadyCommand request, CancellationToken cancellationToken)
        {
            _playerService.SetPlayerIsReady(request.Player);

            return Task.CompletedTask;
        }
    }
}
