using Stream.Models;

namespace Stream.ViewModels.Dto;

public class GameDto
{
    public int Id { get; set; }
    
    public string? Title { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public Platform Platform { get; set; }

    public Genre Genre { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}