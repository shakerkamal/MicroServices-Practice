namespace CommandService.Dtos;

public record CommandReadDto
{
    public Guid Id { get; init; }
    public string HowTo { get; init; }
    public string CommandLine { get; init; }
    public Guid PlatformId { get; init; }
}