using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(200);
            email.HasIndex(e => e.Value).IsUnique();
        });
        
        // Value Object: PhoneNumber
        builder.OwnsOne(c => c.PhoneNumber, phone =>
        {
            phone.Property(p => p.Value)
                .HasColumnName("Phone")
                .IsRequired()
                .HasMaxLength(15);
        });

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasIndex(c => c.Name);
    }
}