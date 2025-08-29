namespace FinanceMath.Domain.ContentAggregate
{
    public class Content
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Body { get; protected set; }
        public virtual string? MediaUrl { get; protected set; }
        public virtual Guid CategoryId { get; protected set; }

        public virtual Guid CreatedBy { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime? UpdatedAt { get; protected set; }

        public virtual Category Category { get; protected set; }
        public virtual ICollection<Exercise> Exercises { get; protected set; } = new List<Exercise>();
        public virtual ICollection<ContentSection> Sections { get; protected set; } = new List<ContentSection>();

        protected Content() { }

        public Content(string title, string body, Guid categoryId, Guid createdBy, string? mediaUrl = null)
        {
            Id = Guid.NewGuid();
            Title = title;
            Body = body;
            CategoryId = categoryId;
            CreatedBy = createdBy;
            MediaUrl = mediaUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public virtual void Update(string title, string body, string? mediaUrl = null)
        {
            Title = title;
            Body = body;
            MediaUrl = mediaUrl;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
