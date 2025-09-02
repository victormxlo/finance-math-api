using FinanceMath.Application.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Contents.Queries
{
    public class GetContentByIdQuery : IRequest<Result<ContentDto>>
    {
        public Guid Id { get; set; }
    }
}
