﻿using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IWebSocketCommandService
    {
        public void Handle(WebSocketRequestMessage data, Player player, Lobby lobby);

        public void ImmitateHandle(ImmitateHandleDTO data);
    }
}
