using Microsoft.AspNetCore.Mvc.Rendering;
using Stream.Models;
using Stream.Repository.Game;
using Stream.Repository.Library;
using Stream.Repository.User;
using Stream.Services.Interfaces;

namespace Stream.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;

        public LibraryService(ILibraryRepository libraryRepository, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _libraryRepository = libraryRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        public async Task<List<Library>> GetAllAsync(string searchQuery = null, int pageNumber = 1, int pageSize = 10)
        {
            var libraries = await _libraryRepository.GetAllAsync(searchQuery);
            return libraries
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        public async Task<SelectList> GetUsersSelectListAsync(int? selectedUserId = null)
        {
            var users = await _userRepository.GetAllAsync();
            return new SelectList(users, "Id", "Username", selectedUserId);
        }

        public async Task<SelectList> GetGamesSelectListAsync(int? selectedGameId = null)
        {
            var games = await _gameRepository.GetAllAsync();
            return new SelectList(games, "Id", "Title", selectedGameId);
        }

        public async Task<int?> GetTotalCountAsync(string searchQuery)
        {
            return await _libraryRepository.GetTotalCountAsync(searchQuery);
        }

        public async Task<Library> GetByIdAsync(int id)
        {
            return await _libraryRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Library library)
        {
            await _libraryRepository.AddAsync(library);
        }

        public async Task UpdateAsync(Library library)
        {
            await _libraryRepository.UpdateAsync(library);
        }

        public async Task DeleteAsync(int id)
        {
            await _libraryRepository.DeleteAsync(id);
        }
        
    }
}