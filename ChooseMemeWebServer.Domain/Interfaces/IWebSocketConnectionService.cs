using ChooseMemeWebServer.Application.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IWebSocketConnectionService
    {
        public void AddPlayerConnection(Player player, WebSocket webSocket);

        public void RemovePlayerConnection(Player player);

        public bool TryGetPlayerConnection(Player player, [MaybeNullWhen(false)] out WebSocket webSocket);

        public void AddServerConnection(Server server, WebSocket webSocket);

        public void RemoveServerConnection(Server server);

        public bool TryGetServerConnection(Server server, [MaybeNullWhen(false)] out WebSocket webSocket);
    }
}
