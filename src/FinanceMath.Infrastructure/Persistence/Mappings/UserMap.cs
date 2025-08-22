using FinanceMath.Domain.Entities.Users;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("users");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Username).Not.Nullable();
            Map(x => x.FullName).Not.Nullable();
            Component(x => x.Email, m => m.Map(e => e.Value).Column("email").Not.Nullable());
            Map(x => x.PasswordHash).Not.Nullable();
            Map(x => x.CreatedAt).Not.Nullable();
            Map(x => x.UpdatedAt);
        }
    }
}
