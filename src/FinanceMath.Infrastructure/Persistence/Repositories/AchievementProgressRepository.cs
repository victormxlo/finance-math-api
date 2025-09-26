using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class AchievementProgressRepository : IAchievementProgressRepository
    {
        private readonly ISession _session;

        public AchievementProgressRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<AchievementProgress>> GetByAchievementId(Guid achievementId)
        {
            return await _session.Query<AchievementProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Achievement)
                .Where(ap => ap.Achievement.Id == achievementId)
                .ToListAsync();
        }

        public async Task<ICollection<AchievementProgress>> GetByGamificationProfileId(Guid gamificationProfileId)
        {
            return await _session.Query<AchievementProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Achievement)
                .Where(ap => ap.GamificationProfile.Id == gamificationProfileId)
                .ToListAsync();
        }

        public async Task<ICollection<AchievementProgress>> GetByUserId(Guid userId)
        {
            return await _session.Query<AchievementProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Achievement)
                .Where(ap => ap.GamificationProfile.User.Id == userId)
                .ToListAsync();
        }
    }
}
