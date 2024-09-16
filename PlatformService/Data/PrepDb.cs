using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext context)
    {
        if(!context.Platforms.Any())
        {
            Console.WriteLine("---> Seeding data...");

            context.Platforms.AddRange(
                new Platform() {Id=Guid.NewGuid(), Name="DotNet", Publisher="Micorsoft",Cost="Free"},
                new Platform() {Id=Guid.NewGuid(), Name="SQL Server", Publisher="Microsoft", Cost="Free"},
                new Platform(){Id=Guid.NewGuid(), Name="Kubernetes", Publisher="Cloud Native Computing Foundation", Cost="Free"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("---> Already have data");
        }
    }
}