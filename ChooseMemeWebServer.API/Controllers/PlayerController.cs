using AutoMapper;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class PlayerController(IPlayerService playerService, IMapper mapper) : ControllerBase
    {
        [HttpGet("/GetPlayers")]
        public IActionResult GetPlayers()
        {
            return Ok(mapper.Map<List<PlayerDTO>>(playerService.GetOnlinePlayers()));
        }

        [HttpGet("/GetBots")]
        public IActionResult GetBots()
        {
            return Ok(mapper.Map<List<PlayerDTO>>(playerService.GetOnlineBots()));
        }
    }
}
