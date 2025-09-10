namespace FinanceMath.Domain.ContentAggregate
{
    public class ExerciseOption
    {
        public virtual Guid Id { get; protected set; }
        public virtual Exercise Exercise { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual int Order { get; protected set; }
        public virtual bool IsCorrect { get; protected set; }

        protected ExerciseOption() { }

        public ExerciseOption(Exercise exercise, string description, int order, bool isCorrect = false)
        {
            Id = Guid.NewGuid();
            Exercise = exercise;
            Description = description;
            Order = order;
            IsCorrect = isCorrect;
        }

        public virtual void Update(string description, int order, bool isCorrect)
        {
            Description = description;
            Order = order;
            IsCorrect = isCorrect;
        }

        public virtual void MarkCorrect()
            => IsCorrect = true;

        public virtual void UnmarkCorrect()
            => IsCorrect = false;
    }
}
