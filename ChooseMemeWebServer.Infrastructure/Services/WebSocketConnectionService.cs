using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class WebSocketConnectionService : IWebSocketConnectionService
    {
        public static Dictionary<Player, WebSocket> onlinePlayers = new Dictionary<Player, WebSocket>();

        public static Dictionary<Lobby, WebSocket> onlineServers = new Dictionary<Lobby, WebSocket>();

        public void AddPlayerConnection(Player player, WebSocket webSocket)
        {
            onlinePlayers.Add(player, webSocket);
        }

        public void RemovePlayerConnection(Player player)
        {
            onlinePlayers.Remove(player);
        }

        public bool TryGetPlayerConnection(Player player, [MaybeNullWhen(false)] out WebSocket webSocket)
        {
            return onlinePlayers.TryGetValue(player, out webSocket);
        }

        public void AddServerConnection(Lobby lobby, WebSocket webSocket)
        {
            onlineServers.Add(lobby, webSocket);
        }

        public void RemoveServerConnection(Lobby lobby)
        {
            onlineServers.Remove(lobby);
        }

        public bool TryGetServerConnection(Lobby lobby, [MaybeNullWhen(false)] out WebSocket webSocket)
        {
            return onlineServers.TryGetValue(lobby, out webSocket);
        }
    }
}
