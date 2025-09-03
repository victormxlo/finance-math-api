using FinanceMath.Application.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; set; }
    }
}
