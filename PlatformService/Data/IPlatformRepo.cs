using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatformRepo
{
    bool Save();

    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(Guid platformId);
    void CreatePlatForm(Platform platform);
}