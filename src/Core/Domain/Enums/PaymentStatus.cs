namespace Mriguel.Domain.Enums
{
    /// <summary>
    /// Represents the status of a payment
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Payment is pending
        /// </summary>
        Pending,
        
        /// <summary>
        /// Payment is completed
        /// </summary>
        Completed,
        
        /// <summary>
        /// Payment is failed
        /// </summary>
        Failed,
        
        /// <summary>
        /// Payment is refunded
        /// </summary>
        Refunded,
        
        /// <summary>
        /// Payment is cancelled
        /// </summary>
        Cancelled
    }
}
