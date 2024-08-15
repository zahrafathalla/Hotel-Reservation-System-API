using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Data.Configuration
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.Status)
                   .HasConversion
                   (
                        status => status.ToString(),
                        status => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), status)
                   );

            builder.Property(r => r.TotalAmount)
               .HasColumnType("decimal(12,2)");
        }
    }
}
