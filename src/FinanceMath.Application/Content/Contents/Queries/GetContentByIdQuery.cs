using FinanceMath.Application.Content.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Queries
{
    public class GetContentByIdQuery : IRequest<Result<ContentDto>>
    {
        public Guid Id { get; set; }
    }
}
