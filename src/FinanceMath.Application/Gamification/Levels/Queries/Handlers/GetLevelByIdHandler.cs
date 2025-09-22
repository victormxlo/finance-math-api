using AutoMapper;
using FinanceMath.Application.Gamification.Levels.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Queries.Handlers
{
    public class GetLevelByIdHandler : IRequestHandler<GetLevelByIdQuery, Result<LevelDto>>
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IMapper _mapper;

        public GetLevelByIdHandler(ILevelRepository levelRepository, IMapper mapper)
        {
            _levelRepository = levelRepository;
            _mapper = mapper;
        }

        public async Task<Result<LevelDto>> Handle(GetLevelByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var level = await _levelRepository.GetByIdAsync(request.Id);

                if (level == null)
                    return Result<LevelDto>.Fail($"Level not found with id: {request.Id}.");

                var dto = _mapper.Map<LevelDto>(level);

                return Result<LevelDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<LevelDto>.Fail($"Failed to get level by id: {ex.Message}.");
            }
        }
    }
}
