using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IGamificationProfileRepository
    {
        Task<GamificationProfile> GetByUserIdAsync(Guid userId);
        Task<GamificationProfile> GetByIdAsync(Guid id);
        Task<ICollection<GamificationProfile>> GetTopByExperienceAsync(int top);
        Task AddAsync(GamificationProfile profile);
        Task UpdateAsync(GamificationProfile profile);
    }
}
