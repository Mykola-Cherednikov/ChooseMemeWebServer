﻿using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands
{
    public abstract class PlayerBaseCommand : IPlayerRequest
    {
        public Lobby Lobby { get; set; } = null!;

        public Player Player { get; set; } = null!;
    }
}
