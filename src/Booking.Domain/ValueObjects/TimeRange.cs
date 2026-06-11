namespace Booking.Domain.ValueObjects;

public sealed record TimeRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public TimeRange(DateTime start, DateTime end)
    {
        if(start > end)
            throw new ArgumentException("Start must be before end", nameof(start));
        
        Start = start;
        End = end;
    }

    public TimeSpan Duration => End - Start;
    public bool Overlaps(TimeRange other) => Start < other.End && End > other.Start;
}