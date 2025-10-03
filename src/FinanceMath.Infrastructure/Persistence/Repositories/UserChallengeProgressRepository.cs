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

        public async Task<ICollection<UserChallengeProgress>> GetByChallengeIdAsync(Guid challengeId)
        {
            return await _session.Query<UserChallengeProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Challenge)
                .Where(ap => ap.Challenge.Id == challengeId)
                .ToListAsync();
        }

        public async Task<ICollection<UserChallengeProgress>> GetByGamificationProfileIdAsync(Guid gamificationProfileId)
        {
            return await _session.Query<UserChallengeProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Challenge)
                .Where(ap => ap.GamificationProfile.Id == gamificationProfileId)
                .ToListAsync();
        }

        public async Task<UserChallengeProgress> GetByProfileAndChallengeAsync(Guid profileId, Guid challengeId)
        {
            return await _session.Query<UserChallengeProgress>()
                .FirstOrDefaultAsync(p => p.GamificationProfile.Id == profileId && p.Challenge.Id == challengeId);
        }

        public async Task<ICollection<UserChallengeProgress>> GetByUserIdAsync(Guid userId)
        {
            return await _session.Query<UserChallengeProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Challenge)
                .Where(ap => ap.GamificationProfile.User.Id == userId)
                .ToListAsync();
        }

        public async Task SaveAsync(UserChallengeProgress progress)
        {
            using var tx = _session.BeginTransaction();
            await _session.SaveAsync(progress);
            await tx.CommitAsync();
        }

        public async Task UpdateAsync(UserChallengeProgress progress)
        {
            using var tx = _session.BeginTransaction();
            await _session.UpdateAsync(progress);
            await tx.CommitAsync();
        }
    }
}
