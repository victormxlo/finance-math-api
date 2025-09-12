namespace FinanceMath.Api.Contracts.Requests
{
    public class UpdateContentSectionRequest
    {
        public Guid ContentSectionId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int Order { get; set; }
    }
}