using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class UserChallengeProgressRepository : IUserChallengeProgressRepository
    {
        private readonly ISession _session;

        public UserChallengeProgressRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<UserChallengeProgress>> GetByChallengeId(Guid challengeId)
        {
            return await _session.Query<UserChallengeProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Challenge)
                .Where(ap => ap.Challenge.Id == challengeId)
                .ToListAsync();
        }

        public async Task<ICollection<UserChallengeProgress>> GetByGamificationProfileId(Guid gamificationProfileId)
        {
            return await _session.Query<UserChallengeProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Challenge)
                .Where(ap => ap.GamificationProfile.Id == gamificationProfileId)
                .ToListAsync();
        }

        public async Task<ICollection<UserChallengeProgress>> GetByUserId(Guid userId)
        {
            return await _session.Query<UserChallengeProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Challenge)
                .Where(ap => ap.GamificationProfile.User.Id == userId)
                .ToListAsync();
        }
    }
}
