namespace FinanceMath.Domain.ContentAggregate
{
    public class Exercise
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Question { get; protected set; }
        public virtual IList<string> Options { get; protected set; } = new List<string>();
        public virtual int CorrectOptionIndex { get; protected set; }
        public virtual Content Content { get; protected set; }

        protected Exercise() { }

        public Exercise(
            string question, IList<string> options, int correctOptionIndex, Content content)
        {
            if (options == null || options?.Count < 2)
                throw new ArgumentNullException("Exercise must have at least two options.");

            if (correctOptionIndex < 0 || correctOptionIndex >= options?.Count)
                throw new ArgumentOutOfRangeException(nameof(correctOptionIndex));

            Id = Guid.NewGuid();
            Question = question;
            Options = options!;
            CorrectOptionIndex = correctOptionIndex;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public virtual bool ValidateAnswer(int chosenIndex)
            => chosenIndex == CorrectOptionIndex;
    }
}
