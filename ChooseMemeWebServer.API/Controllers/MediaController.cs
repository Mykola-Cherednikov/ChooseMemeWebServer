﻿using AutoMapper;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Exceptions;
using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChooseMemeWebServer.API.Controllers
{
    public class MediaController(IMediaService mediaService, IMapper mapper) : ControllerBase
    {
        [HttpGet("GetAllMedia")]
        public async Task<IActionResult> GetAllMedia(string presetid)
        {
            try
            {
                return Ok(mapper.Map<List<MediaDTO>>(await mediaService.GetAllMedia(presetid)));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }

        [HttpGet("GetOneMedia")]
        public async Task<IActionResult> GetOneMedia(string mediaId)
        {
            try
            {
                return Ok(mapper.Map<MediaDTO>(await mediaService.GetOneMedia(mediaId)));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }

        [HttpPost("CreateMedia")]
        [Authorize]
        public async Task<IActionResult> CreateMedia(IFormFile file, string presetid)
        {
            try
            {
                return Ok(mapper.Map<MediaDTO>(await mediaService.CreateMedia(file, presetid)));
            }
            catch (ExpectedException ex)
            {
                return BadRequest(new MessageDTO() { Message = ex.Message });
            }
        }
    }
}
