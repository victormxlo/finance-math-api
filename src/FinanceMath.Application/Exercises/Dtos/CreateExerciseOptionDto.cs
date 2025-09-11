namespace FinanceMath.Application.Exercises.Dtos
{
    public class CreateExerciseOptionDto
    {
        public string Description { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
    }
}
