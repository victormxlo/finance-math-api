namespace FinanceMath.Application.Content.Exercises.Dtos
{
    public class UserExerciseProgressDto
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }
        public Guid ExerciseId { get; set; }
        public required string ExerciseQuestion { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
