using FinanceMath.Application.Content.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; set; }
    }
}
