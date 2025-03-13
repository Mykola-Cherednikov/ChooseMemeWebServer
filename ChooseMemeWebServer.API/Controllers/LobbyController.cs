using AutoMapper;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Exceptions;
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
            try
            {
                var lobby = lobbyService.GetLobby(code);

                return Ok(mapper.Map<LobbyDTO>(lobby));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }

        [HttpPost("/CreateLobby")]
        public async Task<IActionResult> CreateLobby(string presetId)
        {
            return Ok(mapper.Map<LobbyDTO>(await lobbyService.CreateLobby(presetId)));
        }

        [HttpPost("/AddBotToLobby")]
        public async Task<IActionResult> AddBotToLobby([FromQuery] string code)
        {
            return Ok(mapper.Map<LobbyDTO>(await lobbyService.AddBotToLobby(code)));
        }
    }
}
