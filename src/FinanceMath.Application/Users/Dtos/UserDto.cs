namespace FinanceMath.Application.Users.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
