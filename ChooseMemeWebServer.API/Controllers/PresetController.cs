using ChooseMemeWebServer.Application.Exceptions;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class PresetController : ControllerBase
    {
        private readonly IPresetService presetService;

        public PresetController(IPresetService presetService)
        {
            this.presetService = presetService;
        }

        public async Task<IActionResult> GetPreset(string id)
        {
            try
            {
                return Ok(await presetService.GetPreset(id));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> CreatePreset(string name)
        {
            try
            {
                return Ok(await presetService.CreatePreset(name));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
