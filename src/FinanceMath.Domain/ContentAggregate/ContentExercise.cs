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

        public override bool Equals(object obj)
        {
            if (obj is not ContentExercise other) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Content?.Id, other.Content?.Id)
                && Equals(Exercise?.Id, other.Exercise?.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Content?.Id.GetHashCode() ?? 0);
                hash = hash * 23 + (Exercise?.Id.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
