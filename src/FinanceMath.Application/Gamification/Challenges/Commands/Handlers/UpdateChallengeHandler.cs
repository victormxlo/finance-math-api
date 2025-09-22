using AutoMapper;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands.Handlers
{
    public class UpdateChallengeHandler : IRequestHandler<UpdateChallengeCommand, Result<ChallengeDto>>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IMapper _mapper;

        public UpdateChallengeHandler(IChallengeRepository challengeRepository, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _mapper = mapper;
        }

        public async Task<Result<ChallengeDto>> Handle(UpdateChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var challenge = await _challengeRepository.GetByIdAsync(request.Id);

                if (challenge == null)
                    return Result<ChallengeDto>.Fail($"Challenge not found with id: {request.Id}.");

                challenge.Update(
                    request.Name, request.Description, request.CriteriaKey,
                    request.ExperienceReward, request.VirtualCurrencyReward,
                    request.StartDate, request.EndDate);

                await _challengeRepository.UpdateAsync(challenge);

                var dto = _mapper.Map<ChallengeDto>(challenge);

                return Result<ChallengeDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ChallengeDto>.Fail($"Failed to update challenge: {ex.Message}.");
            }
        }
    }
}
