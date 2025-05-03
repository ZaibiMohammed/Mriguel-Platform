using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a payment for a rental
    /// </summary>
    public class Payment : AuditableEntity
    {
        private Payment() { } // Required by EF Core
        
        public Payment(decimal amount, string transactionId, Rental rental)
        {
            if (amount <= 0)
                throw new ArgumentException("Payment amount must be positive", nameof(amount));
                
            Amount = amount;
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            RentalId = rental?.Id ?? throw new ArgumentNullException(nameof(rental));
            Rental = rental;
            Status = "Completed"; // Default status
        }
        
        public decimal Amount { get; private set; }
        public string TransactionId { get; private set; }
        public string Status { get; private set; }
        public Guid RentalId { get; private set; }
        public Rental Rental { get; private set; }
        public string FailureReason { get; private set; }
        
        public void UpdateStatus(string status)
        {
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }
        
        public void SetFailureReason(string reason)
        {
            FailureReason = reason;
            Status = "Failed";
        }
    }
}
