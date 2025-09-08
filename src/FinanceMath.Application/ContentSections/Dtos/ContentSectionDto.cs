namespace FinanceMath.Application.ContentSections.Dtos
{
    public class ContentSectionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int Order { get; set; }
        public Guid ContentId { get; set; }
    }
}
