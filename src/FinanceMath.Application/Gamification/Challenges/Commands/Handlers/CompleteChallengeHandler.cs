using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands.Handlers
{
    public class CompleteChallengeHandler : IRequestHandler<CompleteChallengeCommand, Result<CompleteChallengeResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IGamificationService _gamificationService;

        public CompleteChallengeHandler(
            IUserRepository userRepository,
            IChallengeRepository challengeRepository,
            IGamificationService gamificationService)
        {
            _userRepository = userRepository;
            _challengeRepository = challengeRepository;
            _gamificationService = gamificationService;
        }

        public async Task<Result<CompleteChallengeResponseDto>> Handle(CompleteChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user == null)
                    return Result<CompleteChallengeResponseDto>.Fail($"User not found with id: {request.UserId}.");

                var challenge = await _challengeRepository.GetByIdAsync(request.ChallengeId);

                if (challenge == null)
                    return Result<CompleteChallengeResponseDto>.Fail($"Challenge not found with id: {request.ChallengeId}.");

                var response = await _gamificationService.CompleteChallengeAsync(user.Id, challenge.Id);

                return Result<CompleteChallengeResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                return Result<CompleteChallengeResponseDto>.Fail($"Failed to complete challenge: {ex.Message}.");
            }
        }
    }
}
