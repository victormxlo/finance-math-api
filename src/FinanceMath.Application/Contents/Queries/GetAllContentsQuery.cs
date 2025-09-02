using FinanceMath.Application.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Contents.Queries
{
    public class GetAllContentsQuery : IRequest<Result<ICollection<ContentDto>>> { }
}
