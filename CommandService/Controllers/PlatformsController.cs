using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("/api/command/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly ICommandRepo _reposiotry;
    private readonly IMapper _mapper;

    public PlatformsController(ICommandRepo reposiotry, IMapper mapper)
    {
        _reposiotry = reposiotry;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var platforms = _reposiotry.GetPlatforms();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inbound Post # Command Service");

        return Ok("Inbound test of from Platform Controller");
    }
}