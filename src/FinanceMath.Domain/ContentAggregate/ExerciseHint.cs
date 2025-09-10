namespace FinanceMath.Domain.ContentAggregate
{
    public class ExerciseHint
    {
        public virtual Guid Id { get; protected set; }
        public virtual Exercise Exercise { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual int Order { get; protected set; }

        protected ExerciseHint() { }

        public ExerciseHint(Exercise exercise, string description, int order)
        {
            Id = Guid.NewGuid();
            Exercise = exercise;
            Description = description;
            Order = order;
        }

        public virtual void Update(string description, int order)
        {
            Description = description;
            Order = order;
        }
    }
}
