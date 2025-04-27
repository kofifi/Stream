namespace Stream.Repository.Game
{
    public interface IGameRepository
    {
        Task<List<Models.Game>> GetAllAsync(string searchQuery = null, int pageNumber = 1, int pageSize = 10);
        Task<Stream.Models.Game> GetByIdAsync(int id);
        Task AddAsync(Stream.Models.Game game);
        Task UpdateAsync(Stream.Models.Game game);
        Task DeleteAsync(int id);
    }
}