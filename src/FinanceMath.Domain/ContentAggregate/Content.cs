namespace FinanceMath.Domain.ContentAggregate
{
    public class Content
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Body { get; protected set; }
        public virtual string? MediaUrl { get; protected set; }

        public virtual Guid CreatedBy { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime? UpdatedAt { get; protected set; }

        public virtual Category Category { get; protected set; }

        public virtual ICollection<ContentExercise> ContentExercises { get; protected set; } = new List<ContentExercise>();
        public virtual ICollection<ContentSection> Sections { get; protected set; } = new List<ContentSection>();

        protected Content() { }

        public Content(string title, string body, Category category, Guid createdBy, string? mediaUrl = null)
        {
            Id = Guid.NewGuid();
            Title = title;
            Body = body;
            Category = category;
            CreatedBy = createdBy;
            MediaUrl = mediaUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public virtual void Update(string title, string body, Category category, string? mediaUrl = null)
        {
            Title = title;
            Body = body;
            Category = category;
            MediaUrl = mediaUrl;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
