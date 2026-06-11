namespace Booking.Domain.ValueObjects;

public sealed record PhoneNumber
{
    

    public string Value { get; }
    
    public PhoneNumber(string value)
    {
        if(string.IsNullOrEmpty(value))
            throw new ArgumentException("Phone is required", nameof(value));
        
        Value = value.Trim();
    }
    
}