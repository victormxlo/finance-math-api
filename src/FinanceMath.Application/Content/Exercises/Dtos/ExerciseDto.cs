namespace FinanceMath.Application.Content.Exercises.Dtos
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; } = null!;
        public string Difficulty { get; set; } = null!;

        public ICollection<ExerciseOptionPublicDto> Options { get; set; } = new List<ExerciseOptionPublicDto>();

        public ICollection<Guid> ContentIds { get; set; } = new List<Guid>();
    }
}
