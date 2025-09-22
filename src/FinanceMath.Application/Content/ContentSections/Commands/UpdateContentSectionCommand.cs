using FinanceMath.Application.Content.ContentSections.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Commands
{
    public class UpdateContentSectionCommand : IRequest<Result<ContentSectionDto>>
    {
        public Guid ContentId { get; set; }
        public Guid ContentSectionId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int Order { get; set; }
    }
}
