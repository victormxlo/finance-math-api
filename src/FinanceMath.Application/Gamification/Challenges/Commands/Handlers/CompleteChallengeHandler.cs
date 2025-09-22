using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands.Handlers
{
    public class CompleteChallengeHandler : IRequestHandler<CompleteChallengeCommand, Result<Unit>>
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

        public async Task<Result<Unit>> Handle(CompleteChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user == null)
                    return Result<Unit>.Fail($"User not found with id: {request.UserId}.");

                var challenge = await _challengeRepository.GetByIdAsync(request.ChallengeId);

                if (challenge == null)
                    return Result<Unit>.Fail($"Challenge not found with id: {request.ChallengeId}.");

                // TBD: Specific method call
                // await _gamificationService.CompleteChallengeAsync();

                return Result<Unit>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Fail($"Failed to complete challenge: {ex.Message}.");
            }
        }
    }
}
