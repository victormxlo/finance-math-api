using AutoMapper;
using FinanceMath.Application.Gamification.Levels.Dtos;
using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Commands.Handlers
{
    public class CreateLevelHandler : IRequestHandler<CreateLevelCommand, Result<LevelDto>>
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IMapper _mapper;

        public CreateLevelHandler(ILevelRepository levelRepository, IMapper mapper)
        {
            _levelRepository = levelRepository;
            _mapper = mapper;
        }

        public async Task<Result<LevelDto>> Handle(CreateLevelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Level level = new Level(
                    request.Id, request.Name, request.ThresholdExperience);

                await _levelRepository.SaveAsync(level);

                var dto = _mapper.Map<LevelDto>(level);

                return Result<LevelDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<LevelDto>.Fail($"Failed to create level: {ex.Message}.");
            }
        }
    }
}
