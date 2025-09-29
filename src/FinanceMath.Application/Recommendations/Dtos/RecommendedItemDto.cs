namespace FinanceMath.Application.Recommendations.Dtos
{
    public class RecommendedItemDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Type { get; set; }
        public string Difficulty { get; set; }
        public string Category { get; set; }
    }
}
