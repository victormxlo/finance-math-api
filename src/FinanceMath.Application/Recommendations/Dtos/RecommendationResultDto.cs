namespace FinanceMath.Application.Recommendations.Dtos
{
    public class RecommendationResultDto
    {
        public IReadOnlyCollection<RecommendedItemDto> Items { get; }

        public RecommendationResultDto(ICollection<RecommendedItemDto> items)
        {
            Items = items?.ToList() ?? new List<RecommendedItemDto>();
        }
    }
}
