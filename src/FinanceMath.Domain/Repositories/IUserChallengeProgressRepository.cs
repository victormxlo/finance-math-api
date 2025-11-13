using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserChallengeProgressRepository
    {
        Task<ICollection<UserChallengeProgress>> GetAllAsync();
        Task<ICollection<UserChallengeProgress>> GetByChallengeIdAsync(Guid challengeId);
        Task<ICollection<UserChallengeProgress>> GetAllByProfileIdsAsync(ICollection<Guid> profileIds);
        Task<ICollection<UserChallengeProgress>> GetByGamificationProfileIdAsync(Guid gamificationProfileId);
        Task<ICollection<UserChallengeProgress>> GetByUserIdAsync(Guid userId);
        Task<UserChallengeProgress> GetByProfileAndChallengeAsync(Guid profileId, Guid challengeId);
        Task SaveAsync(UserChallengeProgress progress);
        Task UpdateAsync(UserChallengeProgress progress);
    }
}
