using MediatR;

namespace FinanceMath.Application.Content.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
