namespace Booking.Domain.Execptions;

public class CapacityExceededException : DomainException
{
    public CapacityExceededException(Guid scheduleId) : base($"Schedule {scheduleId} has no available capacity")
    {
    }
}