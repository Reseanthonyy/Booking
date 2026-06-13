using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ReservationId)
            .IsRequired();

        // Money Value Object
        builder.OwnsOne(p => p.Amount, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            money.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(p => p.DueDate)
            .IsRequired();
        builder.Property(p => p.PaidDate);
        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(p => p.Method)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(p => p.TransactionId)
            .HasMaxLength(100);

        builder.HasIndex(p => p.ReservationId);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.DueDate);
    }
}