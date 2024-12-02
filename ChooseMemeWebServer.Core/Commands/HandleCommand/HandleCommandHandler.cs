using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Core.Services;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.HandleCommand
{
    public class HandleCommandHandler : IRequestHandler<HandleCommand>
    {
        private readonly IWebSocketCommandService _commandService;

        public HandleCommandHandler(IWebSocketCommandService commandService)
        {
            _commandService = commandService;        
        }

        public Task Handle(HandleCommand request, CancellationToken cancellationToken)
        {
            _commandService.Handle(request.StringCommand, request.Player, request.Lobby);

            return Task.CompletedTask;
        }
    }
}
