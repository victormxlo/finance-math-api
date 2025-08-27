using FinanceMath.Domain.Users.Entities;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("users");
            Id(x => x.Id)
                .CustomSqlType("uuid").GeneratedBy.GuidComb();
            Map(x => x.Username)
                .Not.Nullable();
            Map(x => x.FullName)
                .Column("full_name")
                .Not.Nullable();
            Component(x
                => x.Email, m => m.Map(e => e.Value).Column("email").Not.Nullable());
            Map(x => x.PasswordHash)
                .Column("password_hash")
                .Not.Nullable();
            Map(x => x.Type)
                .CustomType<int>()
                .Column("type_id")
                .Not.Nullable();
            Map(x => x.CreatedAt)
                .Column("created_at")
                .Not.Nullable();
            Map(x => x.UpdatedAt)
                .Column("updated_at");
        }
    }
}
