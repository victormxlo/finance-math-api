using AutoMapper;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Commands.Handlers
{
    public class DeleteLevelHandler : IRequestHandler<DeleteLevelCommand, Result<Unit>>
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IGamificationProfileRepository _gamificationProfileRepository;
        private readonly IMapper _mapper;

        public DeleteLevelHandler(
            ILevelRepository levelRepository,
            IGamificationProfileRepository gamificationProfileRepository,
            IMapper mapper)
        {
            _levelRepository = levelRepository;
            _gamificationProfileRepository = gamificationProfileRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(DeleteLevelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var level = await _levelRepository.GetByIdAsync(request.Id);

                if (level == null)
                    return Result<Unit>.Fail($"Level not found with id: {request.Id}.");

                var profiles = await _gamificationProfileRepository.GetByLevelIdAsync(level.Id);

                if (profiles?.Count > 0)
                    return Result<Unit>.Fail("Cannot delete level because there are profiles with the level.");

                await _levelRepository.DeleteAsync(level);

                return Result<Unit>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Fail($"Failed to delete level: {ex.Message}.");
            }
        }
    }
}
