using AutoMapper;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Queries.Handlers
{
    public class GetChallengeByIdHandler : IRequestHandler<GetChallengeByIdQuery, Result<ChallengeDto>>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IMapper _mapper;

        public GetChallengeByIdHandler(IChallengeRepository challengeRepository, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _mapper = mapper;
        }

        public async Task<Result<ChallengeDto>> Handle(GetChallengeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var challenge = await _challengeRepository.GetByIdAsync(request.Id);

                if (challenge == null)
                    return Result<ChallengeDto>.Fail($"Challenge not found with id: {request.Id}.");

                var dto = _mapper.Map<ChallengeDto>(challenge);

                return Result<ChallengeDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ChallengeDto>.Fail($"Failed to get challenge by id: {ex.Message}.");
            }
        }
    }
}
