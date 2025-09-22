using AutoMapper;
using FinanceMath.Application.Content.Categories.Commands;
using FinanceMath.Application.Content.Categories.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Categories.Commands.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(
            UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.Id);

                if (category == null)
                    return Result<CategoryDto>.Fail("Category not found.");

                if (!string.IsNullOrEmpty(request.Name))
                    category.UpdateName(request.Name);

                if (request.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _categoryRepository
                        .GetByIdAsync(request.ParentCategoryId.Value);

                    if (parentCategory == null)
                        return Result<CategoryDto>.Fail("Parent category not found.");

                    category.SetParentCategory(parentCategory);
                }
                else
                    category.ClearParentCategory();

                await _categoryRepository.UpdateAsync(category);

                var dto = _mapper.Map<CategoryDto>(category);

                return Result<CategoryDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<CategoryDto>.Fail($"Failed to update category: {ex.Message}.");
            }
        }
    }
}
