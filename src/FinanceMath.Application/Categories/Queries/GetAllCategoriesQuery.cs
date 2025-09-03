using FinanceMath.Application.Categories.Dtos;
using MediatR;

namespace FinanceMath.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<Result<ICollection<CategoryDto>>> { }
}
