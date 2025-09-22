using AutoMapper;
using FinanceMath.Application.Gamification.Levels.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Commands.Handlers
{
    public class UpdateLevelHandler : IRequestHandler<UpdateLevelCommand, Result<LevelDto>>
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IMapper _mapper;

        public UpdateLevelHandler(ILevelRepository levelRepository, IMapper mapper)
        {
            _levelRepository = levelRepository;
            _mapper = mapper;
        }

        public async Task<Result<LevelDto>> Handle(UpdateLevelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var level = await _levelRepository.GetByIdAsync(request.Id);

                if (level == null)
                    return Result<LevelDto>.Fail($"Level not found with id: {request.Id}.");

                level.Update(request.Name, request.ThresholdExperience, request.NewId);

                await _levelRepository.UpdateAsync(level);

                var dto = _mapper.Map<LevelDto>(level);

                return Result<LevelDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<LevelDto>.Fail($"Failed to update level: {ex.Message}.");
            }
        }
    }
}
