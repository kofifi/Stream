using Stream.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Services.Interfaces
{
    public interface IGameService
    {
        Task<List<Game>> GetAllAsync(string searchQuery = null, int pageNumber = 1, int pageSize = 10);
        Task<Game> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
    }
}