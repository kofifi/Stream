using Stream.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<List<User>> SearchUserAsync(string searchQuery, int pageNumber = 1, int pageSize = 10);
        Task<int?> GetTotalCountAsync(string searchQuery);
    }
}