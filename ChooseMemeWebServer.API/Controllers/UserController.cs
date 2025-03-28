using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.UserService.Request;
using ChooseMemeWebServer.Application.Exceptions;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDTO data)
        {
            try
            {
                var token = await userService.Login(data);

                return Ok(token);
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO data)
        {
            try
            {
                var token = await userService.Register(data);

                return Ok(token);
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }
    }
}
