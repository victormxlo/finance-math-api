using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IChallengeRepository
    {
        Task<ICollection<Challenge>> GetActiveChallengesAsync();
        Task<Challenge> GetByIdAsync(Guid id);
        Task<ICollection<Challenge>> GetAllAsync();
        Task SaveAsync(Challenge challenge);
        Task UpdateAsync(Challenge challenge);
        Task DeleteAsync(Challenge challenge);
    }
}
