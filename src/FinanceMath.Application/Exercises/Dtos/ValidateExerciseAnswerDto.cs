namespace FinanceMath.Application.Exercises.Dtos
{
    public class ValidateExerciseAnswerDto
    {
        public Guid ExerciseId { get; set; }
        public Guid ExerciseOptionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
