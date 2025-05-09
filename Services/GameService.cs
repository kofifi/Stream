using Stream.Models;
using Stream.Repository.Game;
using Stream.Services.Interfaces;

namespace Stream.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<List<Game>> SearchGamesAsync(string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var games = await _repository.GetAllAsync(searchQuery);
            return games
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Task<int?> GetTotalCountAsync(string searchQuery)
        {
            return _repository.GetTotalCountAsync(searchQuery);
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Game game)
        {
            await _repository.AddAsync(game);
        }

        public async Task UpdateAsync(Game game)
        {
            await _repository.UpdateAsync(game);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}