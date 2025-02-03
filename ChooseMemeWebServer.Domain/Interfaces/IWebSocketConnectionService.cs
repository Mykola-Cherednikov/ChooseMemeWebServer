using ChooseMemeWebServer.Core.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IWebSocketConnectionService
    {
        public void AddPlayerConnection(Player player, WebSocket webSocket);

        public void RemovePlayerConnection(Player player);

        public bool TryGetPlayerConnection(Player player, [MaybeNullWhen(false)] out WebSocket webSocket);

        public void AddServerConnection(Lobby lobby, WebSocket webSocket);

        public void RemoveServerConnection(Lobby lobby);

        public bool TryGetServerConnection(Lobby lobby, [MaybeNullWhen(false)] out WebSocket webSocket);
    }
}
