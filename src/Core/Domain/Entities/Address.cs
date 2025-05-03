using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a physical address
    /// </summary>
    public class Address : AuditableEntity
    {
        private Address() { } // Required by EF Core
        
        public Address(
            string street, 
            string city, 
            string postalCode, 
            string country, 
            User user,
            string apartment = null,
            double? latitude = null,
            double? longitude = null)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            Apartment = apartment;
            Latitude = latitude;
            Longitude = longitude;
            UserId = user?.Id ?? throw new ArgumentNullException(nameof(user));
            User = user;
        }
        
        public string Street { get; private set; }
        public string Apartment { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        public bool IsDefault { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        
        public void Update(
            string street, 
            string city, 
            string postalCode, 
            string country, 
            string apartment = null,
            double? latitude = null,
            double? longitude = null)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            Apartment = apartment;
            Latitude = latitude;
            Longitude = longitude;
        }
        
        public void SetAsDefault()
        {
            IsDefault = true;
        }
        
        public void UnsetDefault()
        {
            IsDefault = false;
        }
    }
}
