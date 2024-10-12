using CommandService.Models;

namespace CommandService.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _dbContext;

    public CommandRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void CreateCommand(Guid platformId, Command command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        command.PlatformId = platformId;
        _dbContext.Commands.Add(command);
    }

    public void CreatePlatform(Platform platform)
    {
        if(platform is null)
        {
            throw new ArgumentNullException(nameof(platform));
        }
        _dbContext.Platforms.Add(platform);
    }

    public Command GetCommand(Guid platformId, Guid id)
    {
        return _dbContext.Commands
            .Where(c => c.Id == id && c.PlatformId == platformId)
            .FirstOrDefault();
    }

    public IEnumerable<Command> GetCommandsForPlatform(Guid platformId)
    {
        return _dbContext.Commands
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.Platform.Name);
    }

    public IEnumerable<Platform> GetPlatforms()
    {
        return _dbContext.Platforms
            .ToList();
    }

    public bool PlatformExists(Guid id)
    {
        return _dbContext.Platforms.Any(p => p.Id == id);
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() >= 0;
    }
}