using MediatR;

namespace ChooseMemeWebServer.Core.Commands.PlayerReady
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
