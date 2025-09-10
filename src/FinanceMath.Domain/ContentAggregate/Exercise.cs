namespace FinanceMath.Domain.ContentAggregate
{
    public class Exercise
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Question { get; protected set; }
        public virtual string Explanation { get; protected set; }
        public virtual string Difficulty { get; protected set; }

        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime? UpdatedAt { get; protected set; }

        public virtual ICollection<ExerciseOption> Options { get; protected set; } = new List<ExerciseOption>();

        public virtual ICollection<ExerciseHint> Hints { get; protected set; } = new List<ExerciseHint>();

        public virtual ICollection<ContentExercise> ContentExercises { get; protected set; } = new List<ContentExercise>();

        protected Exercise() { }

        public Exercise(string question, string explanation, string difficulty)
        {
            Id = Guid.NewGuid();
            Question = question;
            Explanation = explanation;
            Difficulty = difficulty;
            CreatedAt = DateTime.UtcNow;
        }

        #region Domain
        public virtual void Update(string question, string explanation, string difficulty)
        {
            Question = question;
            Explanation = explanation;
            Difficulty = difficulty;
            Update();
        }

        public virtual void Validate()
        {
            if (Options.Count < 2)
                throw new InvalidOperationException("The exercise must have at leat two options.");

            if (Options.Count(o => o.IsCorrect) != 1)
                throw new InvalidOperationException("The exercise must have exactly one correct option.");
        }

        public virtual bool ValidateAnswer(Guid optionId)
        {
            var option = Options.FirstOrDefault(opt => opt.Id == optionId);

            return option?.IsCorrect ?? false;
        }

        protected virtual void Update()
            => UpdatedAt = DateTime.UtcNow;
        #endregion

        #region Options
        public virtual ExerciseOption AddOption(string description, bool isCorrect, int order)
        {
            if (isCorrect && Options.Any(o => o.IsCorrect))
                throw new InvalidOperationException($"There is already a correct option for the exercise {this.Id}.");

            var opt = new ExerciseOption(this, description, order, isCorrect);
            Options.Add(opt);
            Update();
            return opt;
        }

        public virtual void RemoveOption(Guid optionId)
        {
            var opt = Options.FirstOrDefault(o => o.Id == optionId);
            if (opt != null)
            {
                Options.Remove(opt);
                Update();
            }
        }

        public virtual void SetCorrectOption(Guid optionId)
        {
            var target = Options.FirstOrDefault(o => o.Id == optionId)
                         ?? throw new InvalidOperationException($"Option not found with id: {optionId}.");

            foreach (var o in Options)
                o.UnmarkCorrect();

            target.MarkCorrect();
            Update();
        }
        #endregion

        #region Hints
        public virtual ExerciseHint AddHint(string description, int order)
        {
            var hint = new ExerciseHint(this, description, order);

            Hints.Add(hint);
            Update();
            return hint;
        }

        public virtual void RemoveHint(Guid hintId)
        {
            var hint = Hints.FirstOrDefault(x => x.Id == hintId);

            if (hint != null)
            {
                Hints.Remove(hint);
                Update();
            }
        }
        #endregion
    }
}
