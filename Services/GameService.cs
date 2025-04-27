using Stream.Models;
using Stream.Repository.Game;
using Stream.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Game>> GetAllAsync(string searchQuery = null, int pageNumber = 1, int pageSize = 10)
        {
            return await _repository.GetAllAsync(searchQuery, pageNumber, pageSize);
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