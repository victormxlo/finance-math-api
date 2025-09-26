namespace FinanceMath.Application.Content.Contents.Dtos
{
    public class RecommendedContentDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; } = default!;
        public string Reason { get; set; } = default!;
    }

}
