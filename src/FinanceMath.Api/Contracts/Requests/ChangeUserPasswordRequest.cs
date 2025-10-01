namespace FinanceMath.Api.Contracts.Requests
{
    public class ChangeUserPasswordRequest
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}