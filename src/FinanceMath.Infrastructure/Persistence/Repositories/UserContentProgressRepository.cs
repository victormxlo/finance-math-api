using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class UserContentProgressRepository : IUserContentProgressRepository
    {
        private readonly ISession _session;

        public UserContentProgressRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<UserContentProgress>> GetByContentIdAsync(Guid ContentId)
        {
            return await _session.Query<UserContentProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Content)
                .Where(ap => ap.Content.Id == ContentId)
                .ToListAsync();
        }

        public async Task<ICollection<UserContentProgress>> GetByGamificationProfileIdAsync(Guid gamificationProfileId)
        {
            return await _session.Query<UserContentProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Content)
                .Where(ap => ap.GamificationProfile.Id == gamificationProfileId)
                .ToListAsync();
        }

        public async Task<UserContentProgress> GetByProfileAndContentAsync(Guid profileId, Guid ContentId)
        {
            return await _session.Query<UserContentProgress>()
                .FirstOrDefaultAsync(p => p.GamificationProfile.Id == profileId && p.Content.Id == ContentId);
        }

        public async Task<ICollection<UserContentProgress>> GetByUserIdAsync(Guid userId)
        {
            return await _session.Query<UserContentProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Content)
                .Where(ap => ap.GamificationProfile.User.Id == userId)
                .ToListAsync();
        }

        public async Task SaveAsync(UserContentProgress progress)
        {
            using var tx = _session.BeginTransaction();
            await _session.SaveAsync(progress);
            await tx.CommitAsync();
        }

        public async Task UpdateAsync(UserContentProgress progress)
        {
            using var tx = _session.BeginTransaction();
            await _session.UpdateAsync(progress);
            await tx.CommitAsync();
        }
    }
}
