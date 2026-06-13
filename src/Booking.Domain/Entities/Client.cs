using Booking.Domain.Interfaces;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Entities;

public sealed class Client : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Client() { } //Es para EF Core

    public Client(string name, Email email, PhoneNumber phoneNumber)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void UpdateContactInfo(Email email, PhoneNumber phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }
    
    public void UpdateName(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}