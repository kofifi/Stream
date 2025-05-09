using LibraryModel = Stream.Models.Library;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Repository.Library
{
    public interface ILibraryRepository
    {
        Task<List<LibraryModel>> GetAllAsync(string searchQuery = null, int pageNumber = 1, int pageSize = 10);
        Task<int?> GetTotalCountAsync(string searchQuery);
        Task<LibraryModel> GetByIdAsync(int id);
        Task AddAsync(LibraryModel library);
        Task UpdateAsync(LibraryModel library);
        Task DeleteAsync(int id);
    }
}