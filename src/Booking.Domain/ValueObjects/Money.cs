namespace Booking.Domain.ValueObjects;

//record para representar datos, selade porque nadie puede heredar de esta clase
public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    public Money(decimal amount, string currency)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        if(string.IsNullOrEmpty(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));
        
        Amount = amount;
        Currency = currency;
    }

    public override string ToString() => $"{Amount} {Currency}";

}