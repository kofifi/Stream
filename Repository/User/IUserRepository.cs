namespace Stream.Repository.User;

using Stream.Models;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(string searchQuery = null);
    Task<User> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}