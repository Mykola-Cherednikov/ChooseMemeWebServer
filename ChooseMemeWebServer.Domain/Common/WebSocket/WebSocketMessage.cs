﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public abstract class WebSocketMessage
    {
        public string WebSocketData { get; set; } = string.Empty;

        public string MessageTypeName { get; set; } = string.Empty;
    }

    public class WebSocketRequestMessage : WebSocketMessage
    {
        [JsonIgnore]
        public WebSocketMessageRequestType Type { get => (WebSocketMessageRequestType)Enum.Parse(typeof(WebSocketMessageRequestType), MessageTypeName); }
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

    public enum WebSocketMessageRequestType
    {
        PlayerLeave,
        PlayerIsReady,
        ForceStartGame
    }

    public enum WebSocketMessageResponseType
    {
        OnCreateLobby,
        OnPlayerJoin,
        OnPlayerLeave,
        NewLeader,
        OnPlayerIsReady,
        StartGame,
        OnForceStartGame
    }
}
