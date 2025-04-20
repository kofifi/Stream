using Microsoft.EntityFrameworkCore;
using Stream.Models;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // Use a fixed DateTime value for CreatedAt
        var createdAt = new DateTime(2023, 1, 1);  // Static date for seeding

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "Admin", Email = "admin@stream.com", Password = "password123", CreatedAt = createdAt }
        );

        // Seed Games
        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Title = "Game 1", ReleaseDate = new DateTime(2020, 1, 1), Platform = Platform.PC, Genre = Genre.Action, CreatedAt = createdAt },
            new Game { Id = 2, Title = "Game 2", ReleaseDate = new DateTime(2021, 5, 15), Platform = Platform.PlayStation, Genre = Genre.Adventure, CreatedAt = createdAt }
        );

        // Seed Libraries (set only foreign keys)
        modelBuilder.Entity<Library>().HasData(
            new Library { Id = 1, UserId = 1, GameId = 1, Status = Status.Active },
            new Library { Id = 2, UserId = 1, GameId = 2, Status = Status.Active  }
        );
    }
}