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

        public async Task<ICollection<UserContentProgress>> GetAllAsync()
        {
            return await _session.Query<UserContentProgress>()
                .Fetch(cp => cp.GamificationProfile)
                .Fetch(cp => cp.Content)
                .ToListAsync();
        }

        public async Task<ICollection<UserContentProgress>> GetByContentIdAsync(Guid ContentId)
        {
            return await _session.Query<UserContentProgress>()
                .Fetch(cp => cp.GamificationProfile)
                .Fetch(cp => cp.Content)
                .Where(cp => cp.Content.Id == ContentId)
                .ToListAsync();
        }

        public async Task<ICollection<UserContentProgress>> GetByGamificationProfileIdAsync(Guid gamificationProfileId)
        {
            return await _session.Query<UserContentProgress>()
                .Fetch(cp => cp.GamificationProfile)
                .Fetch(cp => cp.Content)
                .Where(cp => cp.GamificationProfile.Id == gamificationProfileId)
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
                .Fetch(cp => cp.GamificationProfile)
                .Fetch(cp => cp.Content)
                .Where(cp => cp.GamificationProfile.User.Id == userId)
                .ToListAsync();
        }

        public async Task<int> CountByIdAsync(Guid contentId)
        {
            var data = await _session.Query<UserContentProgress>()
                .Fetch(cp => cp.GamificationProfile)
                .Fetch(cp => cp.Content)
                .ToListAsync();

            return data.Count;
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
