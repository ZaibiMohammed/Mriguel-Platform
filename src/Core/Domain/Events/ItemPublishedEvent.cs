using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when an item is published
    /// </summary>
    public class ItemPublishedEvent : DomainEvent
    {
        public Item Item { get; }
        
        public ItemPublishedEvent(Item item)
        {
            Item = item;
        }
    }
}
