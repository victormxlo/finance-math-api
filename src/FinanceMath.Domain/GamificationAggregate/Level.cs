namespace FinanceMath.Domain.GamificationAggregate
{
    public class Level
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual int ThresholdExperience { get; protected set; }

        protected Level() { }

        public Level(int id, string name, int thresholdExperience)
        {
            Id = id;
            Name = name;
            ThresholdExperience = thresholdExperience;
        }
    }
}
