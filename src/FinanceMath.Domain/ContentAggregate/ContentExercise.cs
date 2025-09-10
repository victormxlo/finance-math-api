namespace FinanceMath.Domain.ContentAggregate
{
    public class ContentExercise
    {
        public virtual Guid Id { get; protected set; }
        public virtual Content Content { get; protected set; }
        public virtual Exercise Exercise { get; protected set; }

        protected ContentExercise() { }

        public ContentExercise(Content content, Exercise exercise)
        {
            Id = Guid.NewGuid();
            Content = content;
            Exercise = exercise;
        }
    }
}
