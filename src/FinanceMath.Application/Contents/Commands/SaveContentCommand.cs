using FinanceMath.Application.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Contents.Commands
{
    public class SaveContentCommand : IRequest<Result<ContentDto>>
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid CreatedBy { get; set; }
        public string? MediaUrl { get; set; }
    }
}
