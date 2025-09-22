namespace FinanceMath.Application.Content.Exercises.Dtos
{
    public class ExerciseHintDto
    {
        public Guid Id { get; set; }
        public Guid ExerciseId { get; set; }
        public required string Description { get; set; }
        public int Order { get; set; }
    }
}
