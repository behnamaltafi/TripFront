using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sale.Infrastructure.DataBase.Configuration
{
    public class FamilyFriendshipConfiguration : IEntityTypeConfiguration<FamilyFriendship>
    {
        public void Configure(EntityTypeBuilder<FamilyFriendship> builder)
        {

            builder.HasKey(x => x.Id);
        }
    }
}
