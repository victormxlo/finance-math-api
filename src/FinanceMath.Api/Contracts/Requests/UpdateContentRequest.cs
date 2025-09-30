namespace FinanceMath.Api.Contracts.Requests
{
    public class UpdateContentRequest
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public int Order { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CreatedBy { get; set; }
        public string? MediaUrl { get; set; }
        public bool? IsLastInModule { get; set; } = false;
    }
}