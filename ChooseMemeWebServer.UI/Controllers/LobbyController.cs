using ChooseMemeWebServer.Core.Commands.AdministrationCommands.AddBotToLobby;
using ChooseMemeWebServer.Core.Commands.AdministrationCommands.CreateLobby;
using ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetLobbies;
using ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetLobby;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.UI.Controllers
{
    public class LobbyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LobbyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/GetLobbies")]
        public async Task<IActionResult> GetLobbies()
        {
            var response = await _mediator.Send(new GetLobbiesCommand());
            return Ok(response.Lobbies);
        }

        [HttpGet("/GetLobby")]
        public async Task<IActionResult> GetLobby([FromQuery] string code)
        {
            var response = await _mediator.Send(new GetLobbyCommand() { Code = code });
            return Ok(response.Lobby);
        }

        [HttpGet("/CreateLobby")]
        public async Task<IActionResult> CreateLobby()
        {
            var response = await _mediator.Send(new CreateLobbyCommand());
            return Ok(response.Lobby);
        }

        [HttpPost("/AddBotToLobby")]
        public async Task<IActionResult> AddBotToLobby([FromQuery] string code)
        {
            var response = await _mediator.Send(new AddBotToLobbyCommand() { Code = code });
            return Ok(response.Lobby);
        }
    }
}
