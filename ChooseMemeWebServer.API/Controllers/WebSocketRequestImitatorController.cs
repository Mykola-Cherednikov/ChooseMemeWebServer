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

        [HttpPost("/ImmitatePlayerStartGameRequest")]
        public IActionResult ImitatePlayerStartGameRequest(string playerId)
        {
            return ImitatePlayerRequest(
                new ImmitatePlayerHandleDTO() { 
                    PlayerId = playerId, 
                    Message = new PlayerRequestMessage() { 
                        MessageTypeName = PlayerRequestMessageType.StartGame.ToString() 
                    } 
                }
            );
        }

        [HttpPost("/ImmitatePlayerLeaveRequest")]
        public IActionResult ImitatePlayerLeaveRequest(string playerId)
        {
            return ImitatePlayerRequest(
                new ImmitatePlayerHandleDTO()
                {
                    PlayerId = playerId,
                    Message = new PlayerRequestMessage()
                    {
                        MessageTypeName = PlayerRequestMessageType.PlayerLeave.ToString()
                    }
                }
            );
        }

        [HttpPost("/ImmitatePlayerIsReadyRequest")]
        public IActionResult ImitatePlayerIsReadyRequest(string playerId)
        {
            return ImitatePlayerRequest(
                new ImmitatePlayerHandleDTO()
                {
                    PlayerId = playerId,
                    Message = new PlayerRequestMessage()
                    {
                        MessageTypeName = PlayerRequestMessageType.PlayerIsReady.ToString()
                    }
                }
            );
        }

        [HttpPost("/ImitateServerRequest")]
        public IActionResult ImitateServerRequest([FromBody]ImmitateServerHandleDTO data)
        {
            requestService.ImmitateHandleServerRequest(data);
            return Ok();
        }
    }
}
