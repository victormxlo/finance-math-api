using FinanceMath.Application.Content.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
