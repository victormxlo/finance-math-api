using AutoMapper;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Commands.Handlers
{
    public class DeleteAchievementHandler : IRequestHandler<DeleteAchievementCommand, Result<Unit>>
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public DeleteAchievementHandler(IAchievementRepository achievementRepository, IMapper mapper)
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(DeleteAchievementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var achievement = await _achievementRepository.GetByIdAsync(request.Id);

                if (achievement == null)
                    return Result<Unit>.Fail($"Achievement not found with id: {request.Id}.");

                await _achievementRepository.DeleteAsync(achievement);

                return Result<Unit>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Fail($"Failed to delete achievement: {ex.Message}.");
            }
        }
    }
}
