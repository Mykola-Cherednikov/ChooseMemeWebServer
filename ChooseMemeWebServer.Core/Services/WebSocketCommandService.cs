using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;
using System.Reflection;
using System.Text.Json;

namespace ChooseMemeWebServer.Core.Services
{
    public class WebSocketCommandService : IWebSocketCommandService
    {
        private static Dictionary<string, Type> _commandToCommandTypeCache = new Dictionary<string, Type>();

        private IMediator _mediator;

        public WebSocketCommandService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Handle(WebSocketData data, Player player, Lobby lobby)
        {
            if (!TryGetCommandType(data.CommandTypeName, out var dataType))
            {
                return;
            }

            var entity = JsonSerializer.Deserialize(string.IsNullOrEmpty(data.Data) ? "{}" : data.Data, dataType);

            if (entity == null)
            {
                return;
            }

            IPlayerRequest request = (IPlayerRequest)entity;

            request.Player = player;
            request.Lobby = lobby;

            _mediator.Send((IPlayerRequest)entity);
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
