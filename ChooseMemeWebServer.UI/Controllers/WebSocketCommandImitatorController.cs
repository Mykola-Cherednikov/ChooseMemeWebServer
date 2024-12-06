using ChooseMemeWebServer.Core.Commands.AdministrationCommands.ImitateWebSocketCommand;
using ChooseMemeWebServer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.UI.Controllers
{
    public class WebSocketCommandImitatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WebSocketCommandImitatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/ImitateWebSocketCommand")]
        public IActionResult ImitateWebSocketCommand(WebSocketData webSocketData, string playerId)
        {
            _mediator.Send(new ImitateWebSocketCommandCommand() { WebSocketData = webSocketData, PlayerId = playerId });
            return Ok();
        }
    }
}
