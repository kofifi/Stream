using Microsoft.EntityFrameworkCore;
using Stream.Data;
using Stream.Helpers;
using Stream.Models;

namespace Stream.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stream.Models.User>> GetAllAsync(string searchQuery = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                DateTime? parsedDate = DateTime.TryParse(searchQuery, out var date) ? date : null;
                bool isNumeric = int.TryParse(searchQuery, out var numericValue);

                query = query.Where(u =>
                    (u.Username != null && u.Username.ToLower().Contains(lowerSearchQuery)) ||
                    (u.Email != null && u.Email.ToLower().Contains(lowerSearchQuery)) ||
                    (parsedDate.HasValue && u.CreatedAt.Date == parsedDate.Value.Date) ||
                    (u.CreatedAt.Year.ToString().Contains(searchQuery)) ||
                    (isNumeric && u.CreatedAt.Month == numericValue) ||
                    (isNumeric && u.CreatedAt.Day == numericValue) ||
                    (searchQuery.Contains("-") && DateSearchHelper.IsMonthDayMatch(u.CreatedAt, searchQuery)) ||
                    (searchQuery.Contains("-") && DateSearchHelper.IsYearMonthMatch(u.CreatedAt, searchQuery)));
            }

            return await query.ToListAsync();
        }

        public async Task<Stream.Models.User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(Stream.Models.User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stream.Models.User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int?> GetTotalCountAsync(string searchQuery)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();

                query = query.Where(u =>
                    (u.Username != null && u.Username.ToLower().Contains(lowerSearchQuery)) ||
                    (u.Email != null && u.Email.ToLower().Contains(lowerSearchQuery)));
            }

            return await query.CountAsync();
        }
    }
}