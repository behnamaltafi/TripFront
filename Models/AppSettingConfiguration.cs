using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripFront.Models
{
    public class TripFamilyConfiguration : IEntityTypeConfiguration<TripFamily>
    {
        public void Configure(EntityTypeBuilder<TripFamily> builder)
        {

            builder.HasKey(x => x.Id);
        }
    }
}
