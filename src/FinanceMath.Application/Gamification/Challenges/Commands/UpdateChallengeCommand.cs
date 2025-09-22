using FinanceMath.Application.Gamification.Challenges.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands
{
    public class UpdateChallengeCommand : IRequest<Result<ChallengeDto>>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string CriteriaKey { get; set; }
        public int ExperienceReward { get; set; }
        public int VirtualCurrencyReward { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
