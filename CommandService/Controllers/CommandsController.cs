﻿using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/command/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(Guid platformId)
        {
            if(!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _commandRepo.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name ="GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(Guid platformId,Guid commandId)
        {
            if (!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _commandRepo.GetCommand(platformId, commandId);
            if(command == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(Guid platformId, CommandCreateDto commandCreateDto)
        {
            if (!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            } 

            var command = _mapper.Map<Command>(commandCreateDto);
            _commandRepo.CreateCommand(platformId, command);
            _commandRepo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtAction(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
        }
    }
}
