namespace FinanceMath.Application.Content.Contents.Dtos
{
    public class UserContentProgressDto
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }
        public Guid ContentId { get; set; }
        public required string ContentTitle { get; set; }
        public Guid CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
