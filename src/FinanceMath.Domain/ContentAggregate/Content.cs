namespace FinanceMath.Domain.ContentAggregate
{
    public class Content
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Body { get; protected set; }
        public virtual int Order { get; protected set; }
        public virtual string? MediaUrl { get; protected set; }
        public virtual bool IsLastInModule { get; protected set; }

        public virtual Guid CreatedBy { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime? UpdatedAt { get; protected set; }

        public virtual Category Category { get; protected set; }

        public virtual ICollection<ContentExercise> ContentExercises { get; protected set; } = new List<ContentExercise>();
        public virtual ICollection<ContentSection> Sections { get; protected set; } = new List<ContentSection>();

        protected Content() { }

        public Content(string title, string body, int order, Category category, Guid createdBy, string? mediaUrl = null, bool? isLastInModule = false)
        {
            Id = Guid.NewGuid();
            Title = title;
            Body = body;
            Order = order;
            Category = category;
            CreatedBy = createdBy;
            MediaUrl = mediaUrl;
            IsLastInModule = isLastInModule ?? false;
            CreatedAt = DateTime.UtcNow;
        }

        public virtual void Update(string title, string body, int order, Category category, string? mediaUrl = null, bool? isLastInModule = false)
        {
            Title = title;
            Body = body;
            Order = order;
            Category = category;
            MediaUrl = mediaUrl;
            IsLastInModule = isLastInModule ?? false;
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual bool HasLinkedExercise(Exercise exercise)
            => ContentExercises.Any(e => e.Exercise.Id == exercise.Id);

        public virtual void LinkExercise(Exercise exercise)
        {
            if (!HasLinkedExercise(exercise))
                ContentExercises.Add(new ContentExercise(this, exercise));
        }
    }
}
