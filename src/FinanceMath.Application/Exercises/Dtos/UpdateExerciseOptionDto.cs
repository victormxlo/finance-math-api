namespace FinanceMath.Application.Exercises.Dtos
{
    public class UpdateExerciseOptionDto
    {
        public Guid? Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
        public bool Remove { get; set; } = false;
    }
}
