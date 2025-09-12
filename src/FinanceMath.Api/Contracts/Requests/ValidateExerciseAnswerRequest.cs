namespace FinanceMath.Api.Contracts.Requests
{
    public class ValidateExerciseAnswerRequest
    {
        public Guid UserId { get; set; }
        public Guid ExerciseOptionId { get; set; }
    }
}