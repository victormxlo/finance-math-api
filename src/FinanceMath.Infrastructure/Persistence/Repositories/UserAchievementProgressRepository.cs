using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class UserAchievementProgressRepository : IUserAchievementProgressRepository
    {
        private readonly ISession _session;

        public UserAchievementProgressRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<UserAchievementProgress>> GetByAchievementId(Guid achievementId)
        {
            return await _session.Query<UserAchievementProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Achievement)
                .Where(ap => ap.Achievement.Id == achievementId)
                .ToListAsync();
        }

        public async Task<ICollection<UserAchievementProgress>> GetByGamificationProfileId(Guid gamificationProfileId)
        {
            return await _session.Query<UserAchievementProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Achievement)
                .Where(ap => ap.GamificationProfile.Id == gamificationProfileId)
                .ToListAsync();
        }

        public async Task<ICollection<UserAchievementProgress>> GetByUserId(Guid userId)
        {
            return await _session.Query<UserAchievementProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Achievement)
                .Where(ap => ap.GamificationProfile.User.Id == userId)
                .ToListAsync();
        }
    }
}
