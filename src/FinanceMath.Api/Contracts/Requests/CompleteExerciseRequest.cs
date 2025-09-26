namespace FinanceMath.Api.Contracts.Requests
{
    public class CompleteExerciseRequest
    {
        public Guid UserId { get; set; }
        public Guid ExerciseOptionId { get; set; }
        public bool UsedHint { get; set; }
    }
}