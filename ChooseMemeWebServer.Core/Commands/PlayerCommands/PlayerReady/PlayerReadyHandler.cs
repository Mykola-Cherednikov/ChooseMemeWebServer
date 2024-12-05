using MediatR;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands.PlayerReady
{
    public class PlayerReadyHandler : IRequestHandler<PlayerReadyCommand>
    {
        public Task Handle(PlayerReadyCommand request, CancellationToken cancellationToken)
        {
            request.Player.IsReady = true;

            return Task.CompletedTask;
        }
    }
}
