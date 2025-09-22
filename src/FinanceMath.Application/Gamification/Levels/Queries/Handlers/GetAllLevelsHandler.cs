using AutoMapper;
using FinanceMath.Application.Gamification.Levels.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Queries.Handlers
{
    public class GetAllLevelsHandler : IRequestHandler<GetAllLevelsQuery, Result<ICollection<LevelDto>>>
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IMapper _mapper;

        public GetAllLevelsHandler(ILevelRepository levelRepository, IMapper mapper)
        {
            _levelRepository = levelRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<LevelDto>>> Handle(GetAllLevelsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var levels = await _levelRepository.GetAllAsync();

                if (levels == null || levels?.Count == 0)
                    return Result<ICollection<LevelDto>>.Fail("Levels not found.");

                var dtos = _mapper.Map<ICollection<LevelDto>>(levels);

                return Result<ICollection<LevelDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<LevelDto>>.Fail($"Failed to get all levels: {ex.Message}.");
            }
        }
    }
}
