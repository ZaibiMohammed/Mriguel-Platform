using System;
using MediatR;

namespace Mriguel.Domain.Common
{
    /// <summary>
    /// Base class for all domain events
    /// </summary>
    public abstract class DomainEvent : INotification
    {
        /// <summary>
        /// The date and time when this event occurred
        /// </summary>
        public DateTime OccurredOn { get; }
        
        /// <summary>
        /// A unique identifier for this event
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Indicates whether this event has been published
        /// </summary>
        public bool IsPublished { get; set; }
        
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }
    }
}
