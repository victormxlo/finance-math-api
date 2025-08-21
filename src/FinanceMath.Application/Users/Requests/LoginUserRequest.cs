namespace FinanceMath.Application.Users.Requests
{
    internal class LoginUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
