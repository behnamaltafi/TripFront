using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sale.Infrastructure.DataBase.Configuration
{
    public class TripFamilyConfiguration : IEntityTypeConfiguration<TripFamily>
    {
        public void Configure(EntityTypeBuilder<TripFamily> builder)
        {

            builder.HasKey(x => x.Id);
        }
    }
}
