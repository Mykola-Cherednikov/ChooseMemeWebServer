﻿using ChooseMemeWebServer.Commands;
using ChooseMemeWebServer.Interfaces;
using ChooseMemeWebServer.Models;
using MediatR;
using System.Reflection;
using System.Text.Json;

namespace ChooseMemeWebServer.Handlers
{
    public class WebSocketCommandHandler : IWebSocketCommandHandler
    {
        private static Dictionary<string, Type> _commandToCommandTypeCache = new Dictionary<string, Type>();

        private IMediator _mediator;

        public WebSocketCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Handle(string stringCommand, Player player, Lobby lobby)
        {
            var command = JsonSerializer.Deserialize<WebSocketCommand>(stringCommand);

            if (command == null)
            {
                return;
            }

            if (!TryGetCommandType(command.CommandTypeName, out var commandType))
            {
                return;
            }

            var data = JsonSerializer.Deserialize(command.Data, commandType);

            if (data == null)
            {
                return;
            }

            IClientRequest request = (IClientRequest)data;

            request.Player = player;
            request.Lobby = lobby;

            _mediator.Send((IClientRequest)data);
        }

        private bool TryGetCommandType(string typeName, out Type type)
        {
            if (_commandToCommandTypeCache.ContainsKey(typeName))
            {
                type = _commandToCommandTypeCache[typeName];
                return true;
            }

            Type? foundType = null;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foundType = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);

                if (foundType != null)
                {
                    _commandToCommandTypeCache.Add(typeName, foundType);
                    type = foundType;
                    return true;
                }
            }

            type = null!;
            return false;
        }
    }
}