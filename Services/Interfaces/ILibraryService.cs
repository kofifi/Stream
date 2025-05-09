using Stream.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stream.Services.Interfaces
{
    public interface ILibraryService
    {
        Task<List<Library>> GetAllAsync(string searchQuery = null, int pageNumber = 1, int pageSize = 10);
        Task<int?> GetTotalCountAsync(string searchQuery);
        Task<Library> GetByIdAsync(int id);
        Task AddAsync(Library library);
        Task UpdateAsync(Library library);
        Task DeleteAsync(int id);
        Task<SelectList> GetUsersSelectListAsync(int? selectedUserId = null);
        Task<SelectList> GetGamesSelectListAsync(int? selectedGameId = null);
    }
}