using FinanceMath.Application.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
