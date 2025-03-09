using ChooseMemeWebServer.Application.Exceptions;
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
        public PlayerRequestMessageType Type
        {
            get
            {
                try
                {
                    return (PlayerRequestMessageType)Enum.Parse(typeof(PlayerRequestMessageType), MessageTypeName);
                }
                catch (ArgumentException)
                {
                    throw new ExpectedException("Unexisted player request message type");
                }
            }
        }
    }

    public class ServerRequestMessage : WebSocketMessage
    {
        [JsonIgnore]
        public ServerRequestMessageType Type
        {
            get
            {
                try
                {
                    return (ServerRequestMessageType)Enum.Parse(typeof(ServerRequestMessageType), MessageTypeName);
                }
                catch (ArgumentException)
                {
                    throw new ExpectedException("Unexisted player request message type");
                }
            }
        }
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
        StartGame,
        ChooseMedia
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

        ExpectedError,

        OnCreateLobby,
        OnLobbyClose,
        OnPlayerJoin,
        OnPlayerLeave,
        NewLeader,
        OnPlayerIsReady,
        OnStartGame,
        OnChooseMedia,

        AskQuestion,
        AnswerQuestion,
        ShowAnswersToQuestion,
        VotingQuestion,
        ResultQuestion
    }
}
