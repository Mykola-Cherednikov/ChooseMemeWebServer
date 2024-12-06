using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain;
using MediatR;
using System.Text.Json;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands.HandlePlayerCommand
{
    public class HandlePlayerCommandHandler : IRequestHandler<HandlePlayerCommandCommand>
    {
        private readonly IWebSocketCommandService _commandService;

        public HandlePlayerCommandHandler(IWebSocketCommandService commandService)
        {
            _commandService = commandService;
        }

        public Task Handle(HandlePlayerCommandCommand request, CancellationToken cancellationToken)
        {
            var data = JsonSerializer.Deserialize<WebSocketData>(request.WebSocketData);

            if (data == null)
            {
                return Task.CompletedTask;
            }

            _commandService.Handle(data, request.Player, request.Lobby);

            return Task.CompletedTask;
        }
    }
}
