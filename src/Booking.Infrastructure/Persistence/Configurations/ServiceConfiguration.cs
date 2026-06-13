using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.Duration)
            .IsRequired();

        // Value Object: Money
        builder.OwnsOne(s => s.Price, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("Price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            money.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(s => s.MaxCapacity)
            .IsRequired();

        builder.Property(s => s.IsActive)
            .IsRequired();
    }
}