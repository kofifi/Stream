using Stream.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync(string searchQuery = null);
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}