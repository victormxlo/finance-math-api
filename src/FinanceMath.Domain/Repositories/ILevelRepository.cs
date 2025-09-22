using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface ILevelRepository
    {
        Task<ICollection<Level>> GetAllAsync();
        Task<Level> GetByIdAsync(int id);
        Task SaveAsync(Level level);
        Task UpdateAsync(Level level);
        Task DeleteAsync(Level level);
    }
}
