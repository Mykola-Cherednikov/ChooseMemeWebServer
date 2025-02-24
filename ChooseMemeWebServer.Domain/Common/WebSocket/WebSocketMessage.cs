using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public abstract class WebSocketMessage
    {
        public string MessageTypeName { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string WebSocketData { get; set; } = null!;
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
        StartGame
    }

    public enum ServerRequestMessageType
    {
        CloseLobby,
        NextStatus
    }

    public enum WebSocketMessageResponseType
    {
        UserNameIsNullOrEmpty,
        LobbyCodeIsNullOrEmpty,
        CantFindLobby,
        LobbyAlreadyHaveServer,

        OnCreateLobby,
        OnLobbyClose,
        OnPlayerJoin,
        OnPlayerLeave,
        NewLeader,
        OnPlayerIsReady,
        OnStartGame,

        AskQuestion
    }
}
