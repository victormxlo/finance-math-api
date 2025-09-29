using FinanceMath.Application.Recommendations.Dtos;

namespace FinanceMath.Application.Services
{
    public interface IRecommendationService
    {
        Task<RecommendationResultDto> GetRecommendationsAsync(Guid userId, int maxItems = 5);
    }
}
