using CommandService.Models;

namespace CommandService.Data;

public interface ICommandRepo
{
    bool SaveChanges();

    //Platforms
    IEnumerable<Platform> GetPlatforms();
    void CreatePlatform(Platform platform);
    bool PlatformExists(Guid id);

    //Commands
    IEnumerable<Command> GetCommandsForPlatform(Guid platformId);
    Command GetCommand(Guid platformId ,Guid id);
    void CreateCommand(Guid platformId ,Command command);
}