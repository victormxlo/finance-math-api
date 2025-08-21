namespace FinanceMath.Application.Users.Requests
{
    internal class UpdatePasswordRequest
    {
        public Guid Id { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
