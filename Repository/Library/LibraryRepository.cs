using Microsoft.EntityFrameworkCore;
using Stream.Data;
using Stream.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stream.Repository.Library
{
    using LibraryModel = Stream.Models.Library;

    public class LibraryRepository : ILibraryRepository
    {
        private readonly ApplicationDbContext _context;

        public LibraryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LibraryModel>> GetAllAsync(string searchQuery = null)
        {
            var query = _context.Libraries
                .Include(l => l.User)
                .Include(l => l.Game)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                query = query.Where(l =>
                    (l.User != null && l.User.Username != null && l.User.Username.ToLower().Contains(lowerSearchQuery)) ||
                    (l.Game != null && l.Game.Title != null && l.Game.Title.ToLower().Contains(lowerSearchQuery)) ||
                    l.Status.ToString().ToLower().Contains(lowerSearchQuery));
            }

            return await query.ToListAsync();
        }

        public async Task<LibraryModel> GetByIdAsync(int id)
        {
            return await _context.Libraries
                .Include(l => l.User)
                .Include(l => l.Game)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(LibraryModel library)
        {
            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LibraryModel library)
        {
            var existingLibrary = await _context.Libraries.FindAsync(library.Id);
            if (existingLibrary != null)
            {
                existingLibrary.Status = library.Status;
                _context.Libraries.Update(existingLibrary);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var library = await _context.Libraries.FindAsync(id);
            if (library != null)
            {
                _context.Libraries.Remove(library);
                await _context.SaveChangesAsync();
            }
        }
    }
}