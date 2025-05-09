using Stream.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
        Task<List<Game>> SearchGamesAsync(string searchQuery, int pageNumber = 1, int pageSize = 10);
        Task<int?> GetTotalCountAsync(string searchQuery);
    }
}