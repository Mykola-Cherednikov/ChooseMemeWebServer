using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class LobbyController(ILobbyService lobbyService) : ControllerBase
    {
        [HttpGet("/GetLobbies")]
        public async Task<IActionResult> GetLobbies()
        {
            return Ok(lobbyService.GetLobbies());
        }

        [HttpGet("/GetLobby")]
        public async Task<IActionResult> GetLobby([FromQuery] string code)
        {
            return Ok(lobbyService.GetLobby(code));
        }

        [HttpGet("/CreateLobby")]
        public IActionResult CreateLobby()
        {
            return Ok(lobbyService.CreateLobby());
        }

        [HttpPost("/AddBotToLobby")]
        public IActionResult AddBotToLobby([FromQuery] string code)
        {
            return Ok(lobbyService.AddBotToLobby(code));
        }
    }
}
