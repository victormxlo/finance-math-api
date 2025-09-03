using AutoMapper;
using FinanceMath.Application.Categories.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Categories.Queries.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, Result<ICollection<CategoryDto>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories == null || categories?.Count == 0)
                return Result<ICollection<CategoryDto>>.Fail("No category found.");

            var dtos = _mapper.Map<ICollection<CategoryDto>>(categories);

            return Result<ICollection<CategoryDto>>.Ok(dtos);
        }
    }
}
