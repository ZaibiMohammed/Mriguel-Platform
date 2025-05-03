using System;
// Note: This requires the Microsoft.Extensions.Identity.Stores package
using Microsoft.AspNetCore.Identity;

namespace Mriguel.Domain.Entities.Identity
{
    /// <summary>
    /// Represents an application user in the identity system
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastLoginAt { get; set; }
        
        public string FullName => $"{FirstName} {LastName}";
    }
}
