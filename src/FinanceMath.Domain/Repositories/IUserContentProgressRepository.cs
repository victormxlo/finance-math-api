using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserContentProgressRepository
    {
        Task<ICollection<UserContentProgress>> GetAllAsync();
        Task<ICollection<UserContentProgress>> GetByContentIdAsync(Guid contentId);
        Task<ICollection<UserContentProgress>> GetByGamificationProfileIdAsync(Guid gamificationProfileId);
        Task<ICollection<UserContentProgress>> GetByUserIdAsync(Guid userId);
        Task<UserContentProgress> GetByProfileAndContentAsync(Guid profileId, Guid contentId);
        Task<int> CountByIdAsync(Guid contentId);
        Task SaveAsync(UserContentProgress progress);
        Task UpdateAsync(UserContentProgress progress);
    }
}
