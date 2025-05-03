using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when an item is updated
    /// </summary>
    public class ItemUpdatedEvent : DomainEvent
    {
        public Item Item { get; }
        
        public ItemUpdatedEvent(Item item)
        {
            Item = item;
        }
    }
}
