using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class UserExerciseProgressRepository : IUserExerciseProgressRepository
    {
        private readonly ISession _session;

        public UserExerciseProgressRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<UserExerciseProgress>> GetByExerciseIdAsync(Guid exerciseId)
        {
            return await _session.Query<UserExerciseProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Exercise)
                .Where(ap => ap.Exercise.Id == exerciseId)
                .ToListAsync();
        }

        public async Task<ICollection<UserExerciseProgress>> GetByGamificationProfileIdAsync(Guid gamificationProfileId)
        {
            return await _session.Query<UserExerciseProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Exercise)
                .Where(ap => ap.GamificationProfile.Id == gamificationProfileId)
                .ToListAsync();
        }

        public async Task<UserExerciseProgress> GetByProfileAndExerciseAsync(Guid profileId, Guid exerciseId)
        {
            return await _session.Query<UserExerciseProgress>()
                .FirstOrDefaultAsync(p => p.GamificationProfile.Id == profileId && p.Exercise.Id == exerciseId);
        }

        public async Task<ICollection<UserExerciseProgress>> GetByUserIdAsync(Guid userId)
        {
            return await _session.Query<UserExerciseProgress>()
                .Fetch(ap => ap.GamificationProfile)
                .Fetch(ap => ap.Exercise)
                .Where(ap => ap.GamificationProfile.User.Id == userId)
                .ToListAsync();
        }

        public async Task SaveAsync(UserExerciseProgress progress)
        {
            using var tx = _session.BeginTransaction();
            await _session.SaveAsync(progress);
            await tx.CommitAsync();
        }

        public async Task UpdateAsync(UserExerciseProgress progress)
        {
            using var tx = _session.BeginTransaction();
            await _session.UpdateAsync(progress);
            await tx.CommitAsync();
        }
    }
}
