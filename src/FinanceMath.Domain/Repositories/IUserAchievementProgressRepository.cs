using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserAchievementProgressRepository
    {
        Task<ICollection<UserAchievementProgress>> GetByAchievementId(Guid achievementId);
        Task<ICollection<UserAchievementProgress>> GetByGamificationProfileId(Guid gamificationProfileId);
        Task<ICollection<UserAchievementProgress>> GetByUserId(Guid userId);
    }
}
