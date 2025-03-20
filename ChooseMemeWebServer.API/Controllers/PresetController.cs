using AutoMapper;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Exceptions;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class PresetController(IPresetService presetService, IMapper mapper) : ControllerBase
    {
        [HttpGet("GetPresets")]
        public async Task<IActionResult> GetPresets()
        {
            try
            {
                return Ok(mapper.Map<List<PresetDTO>>(await presetService.GetPresets()));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }

        [HttpGet("GetPreset")]
        public async Task<IActionResult> GetPreset(string id)
        {
            try
            {
                return Ok(mapper.Map<PresetDTO>(await presetService.GetPreset(id)));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }

        [HttpPost("CreatePreset")]
        public async Task<IActionResult> CreatePreset(string name)
        {
            try
            {
                return Ok(mapper.Map<PresetDTO>(await presetService.CreatePreset(name)));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }
    }
}
