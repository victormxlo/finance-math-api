namespace FinanceMath.Domain.GamificationAggregate
{
    public class Achievement
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string CriteriaKey { get; protected set; }
        public virtual int ExperienceReward { get; protected set; }
        public virtual int VirtualCurrencyReward { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }

        protected Achievement() { }

        public Achievement(
            string name, string description, string criteriaKey, int experienceReward, int virtualCurrencyReward)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CriteriaKey = criteriaKey;
            ExperienceReward = experienceReward;
            VirtualCurrencyReward = virtualCurrencyReward;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
