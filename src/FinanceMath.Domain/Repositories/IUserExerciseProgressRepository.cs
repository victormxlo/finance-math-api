using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserExerciseProgressRepository
    {
        Task<ICollection<UserExerciseProgress>> GetByExerciseIdAsync(Guid exerciseId);
        Task<ICollection<UserExerciseProgress>> GetByGamificationProfileIdAsync(Guid gamificationProfileId);
        Task<ICollection<UserExerciseProgress>> GetByUserIdAsync(Guid userId);
        Task<UserExerciseProgress> GetByProfileAndExerciseAsync(Guid profileId, Guid exerciseId);
        Task SaveAsync(UserExerciseProgress progress);
        Task UpdateAsync(UserExerciseProgress progress);
    }
}
