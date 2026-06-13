using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ClientId)
            .IsRequired();
        builder.Property(r => r.ScheduleId)
            .IsRequired();
        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(r => r.CreatedAt)
            .IsRequired();
        builder.Property(r => r.Notes)
            .HasMaxLength(500);

        builder.HasIndex(r => r.ClientId);
        builder.HasIndex(r => r.ScheduleId);
        builder.HasIndex(r => new { r.ClientId, r.ScheduleId, r.Status });
    }
}