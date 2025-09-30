namespace FinanceMath.Domain.ContentAggregate
{
    public class ContentExercise
    {
        public virtual Content Content { get; protected set; }
        public virtual Exercise Exercise { get; protected set; }

        protected ContentExercise() { }

        public ContentExercise(Content content, Exercise exercise)
        {
            Content = content;
            Exercise = exercise;
        }
    }
}
