﻿using ChooseMemeWebServer.Core.Commands.AdministrationCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.UI.Controllers
{
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/GetOnlinePlayers")]
        public async Task<IActionResult> GetOnlinePlayers()
        {
            var response = await _mediator.Send(new GetPlayersCommand());

            return Ok(response.Players);
        }

        [HttpGet("/GetOnlineBots")]
        public async Task<IActionResult> GetOnlineBots()
        {
            var response = await _mediator.Send(new GetBotsCommand());

            return Ok(response.Bots);
        }
    }
}
