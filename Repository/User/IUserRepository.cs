namespace Stream.Repository.User;

public interface IUserRepository
{
    Task<List<Models.User>> GetAllAsync(string searchQuery = null);
    Task<Stream.Models.User> GetByIdAsync(int id);
    Task AddAsync(Stream.Models.User user);
    Task UpdateAsync(Stream.Models.User user);
    Task DeleteAsync(int id);
    Task<int?> GetTotalCountAsync(string searchQuery);
}