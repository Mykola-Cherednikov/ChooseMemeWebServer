using MediatR;

namespace ChooseMemeWebServer.Core.Commands.ForceGameStart
{
    public class ForceGameStartHandler : IRequestHandler<ForceGameStartCommand>
    {
        public Task Handle(ForceGameStartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
