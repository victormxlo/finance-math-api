namespace FinanceMath.Domain.GamificationAggregate
{
    public class Achievement
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string CriteriaKey { get; protected set; }
        public virtual int RewardExperience { get; protected set; }
        public virtual int RewardVirtualCurrency { get; protected set; }

        protected Achievement() { }

        public Achievement(
            string name, string description, string criteriaKey, int rewardExperience, int rewardVirtualCurrency)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CriteriaKey = criteriaKey;
            RewardExperience = rewardExperience;
            RewardVirtualCurrency = rewardVirtualCurrency;
        }
    }
}
