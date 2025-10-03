using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IChallengeRepository
    {
        Task<ICollection<Challenge>> GetActivesAsync();
        Task<Challenge> GetByIdAsync(Guid id);
        Task<ICollection<Challenge>> GetAllAsync();
        Task<ICollection<Challenge>> GetActiveChallengesByCriteriaAsync(string criteriaKey, DateTime currentDate);
        Task SaveAsync(Challenge challenge);
        Task UpdateAsync(Challenge challenge);
        Task DeleteAsync(Challenge challenge);
    }
}
