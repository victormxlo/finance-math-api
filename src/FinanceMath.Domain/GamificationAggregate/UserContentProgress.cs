using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Domain.GamificationAggregate
{
    public class UserContentProgress
    {
        public virtual GamificationProfile GamificationProfile { get; protected set; }
        public virtual Content Content { get; protected set; }
        public virtual DateTime CompletedAt { get; protected set; }

        protected UserContentProgress() { }

        public UserContentProgress(GamificationProfile gamificationProfile, Content content, DateTime completedAt)
        {
            GamificationProfile = gamificationProfile;
            Content = content;
            CompletedAt = completedAt;
        }

        public override bool Equals(object obj)
        {
            if (obj is not UserContentProgress other) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(GamificationProfile?.Id, other.GamificationProfile?.Id)
                && Equals(Content?.Id, other.Content?.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (GamificationProfile?.Id.GetHashCode() ?? 0);
                hash = hash * 23 + (Content?.Id.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
