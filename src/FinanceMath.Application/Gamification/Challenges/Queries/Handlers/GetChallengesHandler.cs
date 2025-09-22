using AutoMapper;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Queries.Handlers
{
    public class GetChallengesHandler : IRequestHandler<GetAllChallengesQuery, Result<ICollection<ChallengeDto>>>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IMapper _mapper;

        public GetChallengesHandler(IChallengeRepository challengeRepository, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<ChallengeDto>>> Handle(GetAllChallengesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ICollection<Challenge> challenges = request.Active ?
                    await _challengeRepository.GetActivesAsync() :
                    await _challengeRepository.GetAllAsync();

                if (challenges == null || challenges?.Count == 0)
                    return Result<ICollection<ChallengeDto>>.Fail("Challenges not found.");

                var dtos = _mapper.Map<ICollection<ChallengeDto>>(challenges);

                return Result<ICollection<ChallengeDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<ChallengeDto>>.Fail($"Failed to get all challenges: {ex.Message}.");
            }
        }
    }
}
