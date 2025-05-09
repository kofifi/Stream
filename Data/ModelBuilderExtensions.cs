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
            new User { Id = 1, Username = "Admin", Email = "admin@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 2, Username = "User1", Email = "user1@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 3, Username = "User2", Email = "user2@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 4, Username = "User3", Email = "user3@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 5, Username = "User4", Email = "user4@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 6, Username = "User5", Email = "user5@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 7, Username = "User6", Email = "user6@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 8, Username = "User7", Email = "user7@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 9, Username = "User8", Email = "user8@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 10, Username = "User9", Email = "user9@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 11, Username = "User10", Email = "user10@stream.com", Password = "password123", CreatedAt = createdAt },
            new User { Id = 12, Username = "User11", Email = "user11@stream.com", Password = "password123", CreatedAt = createdAt }
        );

        // Seed Games
        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Title = "The Witcher 3: Wild Hunt", ReleaseDate = new DateTime(2015, 5, 19), Platform = Platform.PC, Genre = Genre.RPG, CreatedAt = createdAt },
            new Game { Id = 2, Title = "Cyberpunk 2077", ReleaseDate = new DateTime(2020, 12, 10), Platform = Platform.PC, Genre = Genre.RPG, CreatedAt = createdAt },
            new Game { Id = 3, Title = "Red Dead Redemption 2", ReleaseDate = new DateTime(2018, 10, 26), Platform = Platform.PlayStation, Genre = Genre.Action, CreatedAt = createdAt },
            new Game { Id = 4, Title = "God of War", ReleaseDate = new DateTime(2018, 4, 20), Platform = Platform.PlayStation, Genre = Genre.Action, CreatedAt = createdAt },
            new Game { Id = 5, Title = "The Legend of Zelda: Breath of the Wild", ReleaseDate = new DateTime(2017, 3, 3), Platform = Platform.PlayStation, Genre = Genre.Adventure, CreatedAt = createdAt },
            new Game { Id = 6, Title = "Elden Ring", ReleaseDate = new DateTime(2022, 2, 25), Platform = Platform.PC, Genre = Genre.RPG, CreatedAt = createdAt },
            new Game { Id = 7, Title = "Horizon Zero Dawn", ReleaseDate = new DateTime(2017, 2, 28), Platform = Platform.PlayStation, Genre = Genre.Adventure, CreatedAt = createdAt },
            new Game { Id = 8, Title = "Grand Theft Auto V", ReleaseDate = new DateTime(2013, 9, 17), Platform = Platform.PC, Genre = Genre.Action, CreatedAt = createdAt },
            new Game { Id = 9, Title = "Dark Souls III", ReleaseDate = new DateTime(2016, 4, 12), Platform = Platform.PC, Genre = Genre.RPG, CreatedAt = createdAt },
            new Game { Id = 10, Title = "Overwatch", ReleaseDate = new DateTime(2016, 5, 24), Platform = Platform.PC, Genre = Genre.RPG, CreatedAt = createdAt },
            new Game { Id = 11, Title = "Minecraft", ReleaseDate = new DateTime(2011, 11, 18), Platform = Platform.PC, Genre = Genre.RPG, CreatedAt = createdAt },
            new Game { Id = 12, Title = "Fortnite", ReleaseDate = new DateTime(2017, 7, 25), Platform = Platform.PC, Genre = Genre.Action, CreatedAt = createdAt }            
        );

        // Seed Libraries with randomized UserId
        var random = new Random();
        modelBuilder.Entity<Library>().HasData(
            new Library { Id = 1, UserId = 1, GameId = 1, Status = Status.Active },
            new Library { Id = 2, UserId = 2, GameId = 2, Status = Status.Active },
            new Library { Id = 3, UserId = 3, GameId = 3, Status = Status.Pending },
            new Library { Id = 4, UserId = 4, GameId = 4, Status = Status.Active },
            new Library { Id = 5, UserId = 5, GameId = 5, Status = Status.Pending },
            new Library { Id = 6, UserId = 6, GameId = 6, Status = Status.Active },
            new Library { Id = 7, UserId = 7, GameId = 7, Status = Status.Pending },
            new Library { Id = 8, UserId = 8, GameId = 8, Status = Status.Active },
            new Library { Id = 9, UserId = 9, GameId = 9, Status = Status.Pending },
            new Library { Id = 10, UserId = 10, GameId = 10, Status = Status.Active },
            new Library { Id = 11, UserId = 11, GameId = 11, Status = Status.Pending },
            new Library { Id = 12, UserId = 12, GameId = 12, Status = Status.Active }
        );
    }
}