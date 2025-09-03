using AutoMapper;
using FinanceMath.Application.Categories.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Categories.Queries.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.Id);

                if (category == null)
                    return Result<CategoryDto>.Fail($"No category found with id: {request.Id}.");

                var dto = _mapper.Map<CategoryDto>(category);

                return Result<CategoryDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<CategoryDto>.Fail($"Failed to retrieve category by id: {ex.Message}.");
            }
        }
    }
}
