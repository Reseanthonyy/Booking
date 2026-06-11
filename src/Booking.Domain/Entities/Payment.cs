using Booking.Domain.Enums;
using Booking.Domain.Interfaces;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Entities;

public sealed class Payment : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ReservationId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaidDate { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }
    public string? TransactionId { get; private set; }
    
    private Payment() { }

    public Payment(Guid reservationId, Money amount, DateTime dueDate)
    {
        Id = Guid.NewGuid();
        ReservationId = reservationId;
        Amount = amount;
        Status = PaymentStatus.Pending;
        Method = PaymentMethod.Other;
    }

    public void MarkAsPaid(PaymentMethod method, string? transactionId)
    {
        if (Status != PaymentStatus.Pending)
            throw new ArgumentException("Only pending payment cann be paid");
        
        Status = PaymentStatus.Paid;
        Method = method;
        TransactionId = transactionId;
    }

    public void MarkAsOverDue()
    {
        if (Status == PaymentStatus.Pending && DateTime.UtcNow > DueDate)
            Status = PaymentStatus.Overdue;
    }

    public void Refund()
    {
        if(Status != PaymentStatus.Paid)
            throw new ArgumentException("Only paid payment can be refunded");
        Status = PaymentStatus.Refunded;
    }
}