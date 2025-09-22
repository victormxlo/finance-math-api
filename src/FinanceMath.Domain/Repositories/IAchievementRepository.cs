using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IAchievementRepository
    {
        Task<Achievement> GetByIdAsync(Guid id);
        Task<ICollection<Achievement>> GetByCriteriaAsync(string criteriaKey);
        Task<ICollection<Achievement>> GetAllAsync();
        Task SaveAsync(Achievement achievement);
        Task UpdateAsync(Achievement achievement);
        Task DeleteAsync(Achievement achievement);
    }
}
