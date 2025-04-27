using Stream.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllAsync(string searchQuery = null);
        Task<Game> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
    }
}