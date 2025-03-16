using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class FeatureController(IFeatureService featureService) : ControllerBase
    {
        [HttpPost("ClearUnusedMedia")]
        public async Task<IActionResult> ClearUnusedMedia()
        {
            await featureService.ClearUnusedMedia();
            return Ok();
        }
    }
}
