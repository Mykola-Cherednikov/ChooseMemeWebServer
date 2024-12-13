using MediatR;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands
{
    public class ForceGameStartCommand : PlayerBaseCommand
    {
    }

    public class ForceGameStartHandler : IRequestHandler<ForceGameStartCommand>
    {
        public Task Handle(ForceGameStartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
