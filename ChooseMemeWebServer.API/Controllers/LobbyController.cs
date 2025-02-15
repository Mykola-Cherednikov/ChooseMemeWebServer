using AutoMapper;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class LobbyController(ILobbyService lobbyService, IMapper mapper) : ControllerBase
    {
        [HttpGet("/GetLobbies")]
        public IActionResult GetLobbies()
        {
            return Ok(mapper.Map<List<LobbyDTO>>(lobbyService.GetLobbies()));
        }

        [HttpGet("/GetLobby")]
        public IActionResult GetLobby([FromQuery] string code)
        {
            return Ok(mapper.Map<LobbyDTO>(lobbyService.GetLobby(code)));
        }

        [HttpPost("/CreateLobby")]
        public IActionResult CreateLobby()
        {
            return Ok(mapper.Map<LobbyDTO>(lobbyService.CreateLobby()));
        }

        [HttpPost("/AddBotToLobby")]
        public async Task<IActionResult> AddBotToLobby([FromQuery] string code)
        {
            return Ok(mapper.Map<LobbyDTO>(await lobbyService.AddBotToLobby(code)));
        }
    }
}
