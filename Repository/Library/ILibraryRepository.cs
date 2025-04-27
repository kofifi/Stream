using LibraryModel = Stream.Models.Library;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Repository.Library
{
    public interface ILibraryRepository
    {
        Task<List<LibraryModel>> GetAllAsync(string searchQuery = null);
        Task<LibraryModel> GetByIdAsync(int id);
        Task AddAsync(LibraryModel library);
        Task UpdateAsync(LibraryModel library);
        Task DeleteAsync(int id);
    }
}