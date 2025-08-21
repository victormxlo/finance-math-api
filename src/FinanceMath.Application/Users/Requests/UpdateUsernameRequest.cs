namespace FinanceMath.Application.Users.Requests
{
    internal class UpdateUsernameRequest
    {
        public Guid Id { get; set; }
        public required string NewName { get; set; }
    }
}
