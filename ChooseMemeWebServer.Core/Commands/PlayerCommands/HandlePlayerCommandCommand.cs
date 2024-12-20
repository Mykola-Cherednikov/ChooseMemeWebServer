﻿using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.Interfaces;
using MediatR;
using System.Text.Json;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands
{
    public class HandlePlayerCommandCommand : PlayerBaseCommand
    {
        public string WebSocketData { get; set; } = string.Empty;
    }

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
