using AutoMapper;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands.Handlers
{
    public class DeleteChallengeHandler : IRequestHandler<DeleteChallengeCommand, Result<Unit>>
    {
        private readonly IChallengeRepository _challengeRepository;

        public DeleteChallengeHandler(
            IChallengeRepository challengeRepository,
            IMapper mapper)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteChallengeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var challenge = await _challengeRepository.GetByIdAsync(request.Id);

                if (challenge == null)
                    return Result<Unit>.Fail($"Challenge not found with id: {request.Id}.");

                await _challengeRepository.DeleteAsync(challenge);

                return Result<Unit>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Fail($"Failed to delete challenge: {ex.Message}.");
            }
        }
    }
}

