using Microsoft.EntityFrameworkCore;
using Stream.Data;
using Stream.Helpers;

namespace Stream.Repository.Game
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Game>> GetAllAsync(string searchQuery = null)
        {
            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                DateTime? parsedDate = DateTime.TryParse(searchQuery, out var date) ? date : null;
                bool isNumeric = int.TryParse(searchQuery, out var numericValue);

                query = query.Where(g =>
                    (g.Title != null && g.Title.ToLower().Contains(lowerSearchQuery)) ||
                    g.Genre.ToString().ToLower().Contains(lowerSearchQuery) ||
                    (parsedDate.HasValue && g.ReleaseDate.HasValue && g.ReleaseDate.Value.Date == parsedDate.Value.Date) ||
                    (g.ReleaseDate.HasValue && g.ReleaseDate.Value.Year.ToString().Contains(searchQuery)) ||
                    (g.ReleaseDate.HasValue && isNumeric && g.ReleaseDate.Value.Month == numericValue) ||
                    (g.ReleaseDate.HasValue && isNumeric && g.ReleaseDate.Value.Day == numericValue) ||
                    (g.ReleaseDate.HasValue && searchQuery.Contains("-") && DateSearchHelper.IsMonthDayMatch(g.ReleaseDate.Value, searchQuery)) ||
                    (g.ReleaseDate.HasValue && searchQuery.Contains("-") && DateSearchHelper.IsYearMonthMatch(g.ReleaseDate.Value, searchQuery)) ||
                    g.Platform.ToString().ToLower().Contains(lowerSearchQuery));
            }

            return await query.ToListAsync();
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

        public async Task<int?> GetTotalCountAsync(string searchQuery)
        {
            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                query = query.Where(g =>
                    (g.Title != null && g.Title.ToLower().Contains(lowerSearchQuery)) ||
                    g.Genre.ToString().ToLower().Contains(lowerSearchQuery));
            }

            return await query.CountAsync();
        }
    }
}