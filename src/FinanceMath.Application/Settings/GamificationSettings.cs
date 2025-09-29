namespace FinanceMath.Application.Settings
{
    public class GamificationSettings
    {
        public int XpPerContent { get; set; } = 20;
        public int XpPerExercise { get; set; } = 50;
        public int XpPerExerciseNoHintBonus { get; set; } = 10;
        public int XpPerExerciseIncorrect { get; set; } = 25;
        public int XpPerChallenge { get; set; } = 80;
        public int XpPerModuleComplete { get; set; } = 200;

        public int Streak3DaysXp { get; set; } = 50;
        public int Streak7DaysXp { get; set; } = 300;

        public int VirtualCurrencyPerContent { get; set; } = 5;
        public int VirtualCurrencyPerExercise { get; set; } = 10;
        public int VirtualCurrencyPerChallenge { get; set; } = 15;
        public int VirtualCurrencyPerDailyChallenge { get; set; } = 10;

        public int LeaderboardDefaultTop { get; set; } = 50;
    }
}
