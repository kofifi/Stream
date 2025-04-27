namespace Stream.Repository.GameRepository
{
    public interface IGameRepository
    {
        Task<List<Stream.Models.Game>> GetAllAsync(string searchQuery = null);
        Task<Stream.Models.Game> GetByIdAsync(int id);
        Task AddAsync(Stream.Models.Game game);
        Task UpdateAsync(Stream.Models.Game game);
        Task DeleteAsync(int id);
    }
}