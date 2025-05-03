using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a payment method associated with a user
    /// </summary>
    public class PaymentMethod : AuditableEntity
    {
        private PaymentMethod() { } // Required by EF Core
        
        public PaymentMethod(string type, string lastFourDigits, string token, User user)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            LastFourDigits = lastFourDigits ?? throw new ArgumentNullException(nameof(lastFourDigits));
            Token = token ?? throw new ArgumentNullException(nameof(token));
            UserId = user?.Id ?? throw new ArgumentNullException(nameof(user));
            User = user;
        }
        
        public string Type { get; private set; } // Credit Card, PayPal, etc.
        public string LastFourDigits { get; private set; }
        public string Token { get; private set; } // Payment processor token
        public bool IsDefault { get; private set; }
        public DateTimeOffset? ExpiryDate { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        
        public void SetAsDefault()
        {
            IsDefault = true;
        }
        
        public void UnsetDefault()
        {
            IsDefault = false;
        }
        
        public void SetExpiryDate(DateTimeOffset expiryDate)
        {
            ExpiryDate = expiryDate;
        }
        
        public void UpdateToken(string token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }
    }
}
