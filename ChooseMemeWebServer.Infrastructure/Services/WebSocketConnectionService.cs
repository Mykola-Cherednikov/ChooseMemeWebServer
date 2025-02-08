using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class WebSocketConnectionService : IWebSocketConnectionService
    {
        public static Dictionary<Player, WebSocket> onlinePlayers = new Dictionary<Player, WebSocket>();

        public static Dictionary<Server, WebSocket> onlineServers = new Dictionary<Server, WebSocket>();

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

        public void AddServerConnection(Server server, WebSocket webSocket)
        {
            onlineServers.Add(server, webSocket);
        }

        public void RemoveServerConnection(Server server)
        {
            onlineServers.Remove(server);
        }

        public bool TryGetServerConnection(Server server, [MaybeNullWhen(false)] out WebSocket webSocket)
        {
            return onlineServers.TryGetValue(server, out webSocket);
        }
    }
}
