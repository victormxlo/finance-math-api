namespace FinanceMath.Api.Contracts.Requests
{
    public class CreateContentSectionRequest
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public int Order { get; set; }
    }
}