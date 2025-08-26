using FinanceMath.Domain.Users.Entities;

namespace FinanceMath.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
