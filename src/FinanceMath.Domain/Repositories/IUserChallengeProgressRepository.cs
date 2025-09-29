using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserChallengeProgressRepository
    {
        Task<ICollection<UserChallengeProgress>> GetByChallengeId(Guid challengeId);
        Task<ICollection<UserChallengeProgress>> GetByGamificationProfileId(Guid gamificationProfileId);
        Task<ICollection<UserChallengeProgress>> GetByUserId(Guid userId);
    }
}
