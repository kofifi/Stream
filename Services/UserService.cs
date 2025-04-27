using Stream.Models;
using Stream.Repository.User;
using Stream.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllAsync(string searchQuery = null)
        {
            return await _repository.GetAllAsync(searchQuery);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}