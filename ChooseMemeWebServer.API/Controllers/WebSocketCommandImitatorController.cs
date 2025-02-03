using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class WebSocketCommandImitatorController(IWebSocketCommandService commandService) : ControllerBase
    {
        [HttpPost("/ImitateWebSocketCommand")]
        public IActionResult ImitateWebSocketCommand([FromBody]ImmitateHandleDTO data)
        {
            commandService.ImmitateHandle(data);
            return Ok();
        }
    }
}
