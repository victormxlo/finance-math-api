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
    }
}
