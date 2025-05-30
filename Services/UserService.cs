﻿using Stream.Models;
using Stream.Repository.User;
using Stream.Services.Interfaces;

namespace Stream.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<List<User>> SearchUserAsync(string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var users = await _repository.GetAllAsync(searchQuery);
            return users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Task<int?> GetTotalCountAsync(string searchQuery)
        {
            return _repository.GetTotalCountAsync(searchQuery);
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