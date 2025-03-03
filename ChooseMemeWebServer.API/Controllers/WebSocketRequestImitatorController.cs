using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class WebSocketRequestImitatorController(IWebSocketRequestService requestService) : ControllerBase
    {
        [HttpPost("/ImitatePlayerRequest")]
        public async Task<IActionResult> ImitatePlayerRequest([FromBody]ImmitatePlayerHandleDTO data)
        {
            await requestService.ImmitateHandlePlayerRequest(data);
            return Ok();
        }

        [HttpPost("/ImmitatePlayerStartGameRequest")]
        public async Task<IActionResult> ImitatePlayerStartGameRequest(string playerId)
        {
            return await ImitatePlayerRequest(
                new ImmitatePlayerHandleDTO() { 
                    PlayerId = playerId, 
                    Message = new PlayerRequestMessage() { 
                        MessageTypeName = PlayerRequestMessageType.StartGame.ToString() 
                    } 
                }
            );
        }

        [HttpPost("/ImmitatePlayerLeaveRequest")]
        public async Task<IActionResult> ImitatePlayerLeaveRequest(string playerId)
        {
            return await ImitatePlayerRequest(
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
        public async Task<IActionResult> ImitatePlayerIsReadyRequest(string playerId)
        {
            return await ImitatePlayerRequest(
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
