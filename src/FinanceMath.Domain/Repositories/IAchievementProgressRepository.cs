using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IAchievementProgressRepository
    {
        Task<ICollection<AchievementProgress>> GetByAchievementId(Guid achievementId);
        Task<ICollection<AchievementProgress>> GetByGamificationProfileId(Guid gamificationProfileId);
        Task<ICollection<AchievementProgress>> GetByUserId(Guid userId);
    }
}
