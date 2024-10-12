namespace CommandService.Dtos;


public record PlatformReadDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
} 