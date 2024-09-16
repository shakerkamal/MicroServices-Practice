namespace PlatformService.Dtos;

public record PlatformReadDto
{
    public Guid Id { get; init;}
    public string Name { get; init;}
    public string Publisher { get; init; }
    public string Cost { get; init; }
}