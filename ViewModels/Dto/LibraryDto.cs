using Stream.Models;

namespace Stream.ViewModels.Dto;

public class LibraryDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

    public int GameId { get; set; }

    public Game? Game { get; set; }

    public Status Status { get; set; }
}