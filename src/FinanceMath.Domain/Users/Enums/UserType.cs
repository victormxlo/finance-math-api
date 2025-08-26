using FinanceMath.Domain.Shared;

namespace FinanceMath.Domain.Users.Enums
{
    public class UserType : Enumeration
    {
        public static readonly UserType Admin = new(1, "Admin");
        public static readonly UserType Student = new(2, "Student");

        private UserType(int id, string name) : base(id, name) { }
    }
}
