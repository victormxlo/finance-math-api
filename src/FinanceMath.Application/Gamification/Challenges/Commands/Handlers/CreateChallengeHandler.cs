using AutoMapper;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands.Handlers
{
    public class CreateChallengeHandler : IRequestHandler<CreateChallengeCommand, Result<ChallengeDto>>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IMapper _mapper;

        public CreateChallengeHandler(IChallengeRepository challengeRepository, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _mapper = mapper;
        }

        public async Task<Result<ChallengeDto>> Handle(CreateChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Challenge challenge = new Challenge(
                    request.Name, request.Description, request.CriteriaKey,
                    request.ExperienceReward, request.VirtualCurrencyReward,
                    request.StartDate, request.EndDate);

                await _challengeRepository.SaveAsync(challenge);

                var dto = _mapper.Map<ChallengeDto>(challenge);

                return Result<ChallengeDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ChallengeDto>.Fail($"Failed to create challenge: {ex.Message}.");
            }
        }
    }
}
