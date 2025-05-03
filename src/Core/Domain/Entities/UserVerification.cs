using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a verification document or process for a user
    /// </summary>
    public class UserVerification : AuditableEntity
    {
        private UserVerification() { } // Required by EF Core
        
        public UserVerification(string type, string documentUrl, User user)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            DocumentUrl = documentUrl ?? throw new ArgumentNullException(nameof(documentUrl));
            Status = "Pending"; // Default status
            UserId = user?.Id ?? throw new ArgumentNullException(nameof(user));
            User = user;
        }
        
        public string Type { get; private set; } // ID, Passport, Driver's License, etc.
        public string DocumentUrl { get; private set; }
        public string Status { get; private set; } // Pending, Approved, Rejected
        public string RejectionReason { get; private set; }
        public DateTimeOffset? VerifiedAt { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        
        public void Approve()
        {
            Status = "Approved";
            VerifiedAt = DateTimeOffset.UtcNow;
            RejectionReason = null;
        }
        
        public void Reject(string reason)
        {
            Status = "Rejected";
            RejectionReason = reason ?? throw new ArgumentNullException(nameof(reason));
        }
        
        public void UpdateDocument(string documentUrl)
        {
            DocumentUrl = documentUrl ?? throw new ArgumentNullException(nameof(documentUrl));
            Status = "Pending"; // Reset status when document is updated
            VerifiedAt = null;
            RejectionReason = null;
        }
    }
}
