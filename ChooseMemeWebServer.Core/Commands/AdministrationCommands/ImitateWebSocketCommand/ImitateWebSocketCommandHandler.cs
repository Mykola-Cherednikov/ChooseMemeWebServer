using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.ImitateWebSocketCommand
{
    public class ImitateWebSocketCommandHandler : IRequestHandler<ImitateWebSocketCommandCommand>
    {
        private readonly IWebSocketCommandService _commandService;

        public ImitateWebSocketCommandHandler(IWebSocketCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task Handle(ImitateWebSocketCommandCommand request, CancellationToken cancellationToken)
        {
            _commandService.Handle(request.WebSocketData, null!, null!);

            return Task.CompletedTask;
        }
    }
}
