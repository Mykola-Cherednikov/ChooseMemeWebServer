using MediatR;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands.ForceGameStart
{
    public class ForceGameStartHandler : IRequestHandler<ForceGameStartCommand>
    {
        public Task Handle(ForceGameStartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
