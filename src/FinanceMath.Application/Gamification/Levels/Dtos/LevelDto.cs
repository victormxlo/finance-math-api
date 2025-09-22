namespace FinanceMath.Application.Gamification.Levels.Dtos
{
    public class LevelDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int ThresholdExperience { get; set; }
    }
}
