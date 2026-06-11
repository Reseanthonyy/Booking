namespace Booking.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }
    
    public Email(string value)
    {
        if(string.IsNullOrEmpty(value))
            throw new ArgumentException("Email is required", nameof(value));
        if (!value.Contains("@"))
            throw new ArgumentException("Invalid email format", nameof(value));
        
        
        Value = value.ToLowerInvariant();
    }
}