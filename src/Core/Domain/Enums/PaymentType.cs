namespace Mriguel.Domain.Enums
{
    /// <summary>
    /// Represents the type of payment
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// Credit card payment
        /// </summary>
        CreditCard,
        
        /// <summary>
        /// PayPal payment
        /// </summary>
        PayPal,
        
        /// <summary>
        /// Bank transfer payment
        /// </summary>
        BankTransfer,
        
        /// <summary>
        /// Cash payment
        /// </summary>
        Cash,
        
        /// <summary>
        /// Other payment method
        /// </summary>
        Other
    }
}
