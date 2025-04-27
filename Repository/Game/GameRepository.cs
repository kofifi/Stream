using Microsoft.EntityFrameworkCore;
using Stream.Data;

namespace Stream.Repository.Game
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Game>> GetAllAsync(string searchQuery = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                query = query.Where(g =>
                    (g.Title != null && g.Title.ToLower().Contains(lowerSearchQuery)) ||
                    g.Genre.ToString().ToLower().Contains(lowerSearchQuery));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Stream.Models.Game> GetByIdAsync(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddAsync(Stream.Models.Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stream.Models.Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}