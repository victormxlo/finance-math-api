using FinanceMath.Application.Content.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Queries
{
    public class GetAllContentsQuery : IRequest<Result<ICollection<ContentDto>>> { }
}
