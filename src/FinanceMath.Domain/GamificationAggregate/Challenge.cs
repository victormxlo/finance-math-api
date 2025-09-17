namespace FinanceMath.Domain.GamificationAggregate
{
    public class Challenge
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string CriteriaKey { get; protected set; }
        public virtual int ExperienceReward { get; protected set; }
        public virtual int VirtualCurrencyReward { get; protected set; }
        public virtual DateTime StartDate { get; protected set; }
        public virtual DateTime EndDate { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }

        protected Challenge() { }

        public Challenge(
            string name, string description, string criteriaKey,
            int experienceReward, int virtualCurrencyReward, DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CriteriaKey = criteriaKey;
            ExperienceReward = experienceReward;
            VirtualCurrencyReward = virtualCurrencyReward;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
