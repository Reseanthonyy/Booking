namespace Booking.Domain.Execptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message){}
}