using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public abstract class WebSocketMessage
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string WebSocketData { get; set; } = null!;

        public string MessageTypeName { get; set; } = null!;
    }

    public class PlayerRequestMessage : WebSocketMessage
    {
        [JsonIgnore]
        public PlayerRequestMessageType Type { get => (PlayerRequestMessageType)Enum.Parse(typeof(PlayerRequestMessageType), MessageTypeName); }
    }

    public class ServerRequestMessage : WebSocketMessage
    {
        [JsonIgnore]
        public ServerRequestMessageType Type { get => (ServerRequestMessageType)Enum.Parse(typeof(ServerRequestMessageType), MessageTypeName); }
    }

    public class WebSocketResponseMessage : WebSocketMessage
    {
        public WebSocketResponseMessage(WebSocketMessageResponseType type)
        {
            MessageTypeName = Enum.GetName(typeof(WebSocketMessageResponseType), type)!;
        }

        public WebSocketResponseMessage(WebSocketMessageResponseType type, object data)
        {
            WebSocketData = JsonSerializer.Serialize(data);
            MessageTypeName = Enum.GetName(typeof(WebSocketMessageResponseType), type)!;
        }
    }

    public enum PlayerRequestMessageType
    {
        PlayerLeave,
        PlayerIsReady,
        ForceStartGame
    }

    public enum ServerRequestMessageType
    {
        NextState
    }

    public enum WebSocketMessageResponseType
    {
        OnCreateLobby,
        OnPlayerJoin,
        OnPlayerLeave,
        NewLeader,
        OnPlayerIsReady,
        StartGame,
        OnStartGame
    }
}
