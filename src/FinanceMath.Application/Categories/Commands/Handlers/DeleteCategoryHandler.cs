using AutoMapper;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Categories.Commands.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.Id);

                if (category == null)
                    return Result<bool>.Fail("Category not found.");

                if (category.Subcategories?.Any() ?? false)
                    return Result<bool>.Fail("Category cannot be deleted because it has subcategories.");

                if (category.Contents?.Any() ?? false)
                    return Result<bool>.Fail("Category cannot be deleted because it has associated contents.");

                await _categoryRepository.DeleteAsync(category);

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Failed to delete category: {ex.Message}.");
            }
        }
    }
}
