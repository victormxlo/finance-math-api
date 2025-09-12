namespace FinanceMath.Api.Contracts.Responses
{
    public class ValidateExerciseAnswerResponse
    {
        public Guid ExerciseId { get; set; }
        public Guid ExerciseOptionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}