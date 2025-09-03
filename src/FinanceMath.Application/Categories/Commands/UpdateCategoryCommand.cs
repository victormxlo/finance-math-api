using FinanceMath.Application.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
