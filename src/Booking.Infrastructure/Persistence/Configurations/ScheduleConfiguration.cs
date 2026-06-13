using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.ServiceId)
            .IsRequired();

        // Value Object: TimeRange
        builder.OwnsOne(s => s.TimeRange, tr =>
        {
            tr.Property(t => t.Start)
                .HasColumnName("StartTime")
                .IsRequired();
            tr.Property(t => t.End)
                .HasColumnName("EndTime")
                .IsRequired();
        });

        builder.Property(s => s.MaxCapacity)
            .IsRequired();

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.HasIndex(s => s.ServiceId);
        builder.HasIndex(s => new { s.ServiceId, s.IsActive });
    }
}