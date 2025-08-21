namespace FinanceMath.Application.Users.Dtos
{
    internal class LoginUserResponse
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
        public required UserDto User { get; set; }
    }
}
