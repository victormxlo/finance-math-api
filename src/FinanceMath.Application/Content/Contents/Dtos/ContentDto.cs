namespace FinanceMath.Application.Content.Contents.Dtos
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? MediaUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
