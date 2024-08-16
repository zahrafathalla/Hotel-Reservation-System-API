using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Data.Configuration
{
    public class FacilityConfig : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.Property(f => f.Name).IsRequired();

            builder.Property(f => f.Description).IsRequired();

            builder.Property(f => f.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
