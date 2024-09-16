using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _context;

    public PlatformRepo(AppDbContext context)
    {
        _context = context;
    }
    public void CreatePlatForm(Platform platform)
    {
        if(platform == null){
            throw new ArgumentNullException(nameof(platform));
        }
        _context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public Platform GetPlatformById(Guid platformId)
    {
        return _context.Platforms.FirstOrDefault(p => p.Id== platformId);
    }

    public bool Save()
    {
        return (_context.SaveChanges() >= 0);
    }
}