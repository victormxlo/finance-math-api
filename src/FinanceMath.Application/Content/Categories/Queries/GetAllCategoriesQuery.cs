using FinanceMath.Application.Content.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<Result<ICollection<CategoryDto>>> { }
}
