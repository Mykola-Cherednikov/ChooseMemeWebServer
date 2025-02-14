using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class WebSocketRequestImitatorController(IWebSocketRequestService requestService) : ControllerBase
    {
        [HttpPost("/ImitatePlayerRequest")]
        public IActionResult ImitatePlayerRequest([FromBody]ImmitatePlayerHandleDTO data)
        {
            requestService.ImmitateHandlePlayerRequest(data);
            return Ok();
        }

        [HttpPost("/ImitateServerRequest")]
        public IActionResult ImitateServerRequest([FromBody]ImmitateServerHandleDTO data)
        {
            requestService.ImmitateHandleServerRequest(data);
            return Ok();
        }
    }
}
