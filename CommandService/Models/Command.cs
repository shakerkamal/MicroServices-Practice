using System.ComponentModel.DataAnnotations;

namespace CommandService.Models;

public class Command
{
    public Guid Id { get; set; }
    [Required]
    public string HowTo { get; set; }
    [Required]
    public string CommandLine { get; set; }
    public Guid PlatformId { get; set; }
    public Platform Platform { get; set; }
}