using FinanceMath.Application.Content.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Queries
{
    public class GetUserContentProgressQuery : IRequest<Result<ICollection<UserContentProgressDto>>>
    {
        public Guid UserId { get; set; }
    }
}
