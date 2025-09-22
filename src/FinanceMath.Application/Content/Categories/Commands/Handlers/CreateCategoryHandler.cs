using AutoMapper;
using FinanceMath.Application.Content.Categories.Commands;
using FinanceMath.Application.Content.Categories.Dtos;
using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Categories.Commands.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Category? parentCategory = null;
                if (request?.ParentCategoryId.HasValue ?? false)
                {
                    parentCategory = await _categoryRepository.GetByIdAsync(request.ParentCategoryId.Value);

                    if (parentCategory == null)
                        return Result<CategoryDto>.Fail("Parent category not found.");
                }

                var category = new Category(request!.Name, parentCategory);

                await _categoryRepository.SaveAsync(category);

                var dto = _mapper.Map<CategoryDto>(category);

                return Result<CategoryDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<CategoryDto>.Fail($"Failed to create category: {ex.Message}.");
            }
        }
    }
}
