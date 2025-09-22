using FinanceMath.Application.Content.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
