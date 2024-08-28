using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Configuration
{
    public class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(r => r.Type)
                .HasConversion
                (
                    type => type.ToString(),
                    type => (RoomType)Enum.Parse(typeof(RoomType), type)
                );

            builder.Property(r => r.Price)
                .HasColumnType("decimal(12,2)");
        }
    }
}
