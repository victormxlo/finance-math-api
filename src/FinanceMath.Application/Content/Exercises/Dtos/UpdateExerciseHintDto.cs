namespace FinanceMath.Application.Content.Exercises.Dtos
{
    public class UpdateExerciseHintDto
    {
        public Guid? Id { get; set; }
        public string Description { get; set; } = null!;
        public int Order { get; set; }
        public bool Remove { get; set; } = false;
    }
}
