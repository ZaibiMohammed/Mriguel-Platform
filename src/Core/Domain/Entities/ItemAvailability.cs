using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents the availability period for an item
    /// </summary>
    public class ItemAvailability : AuditableEntity
    {
        private ItemAvailability() { } // Required by EF Core
        
        public ItemAvailability(Item item, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentException("Start date must be before end date");
                
            ItemId = item?.Id ?? throw new ArgumentNullException(nameof(item));
            Item = item;
            StartDate = startDate;
            EndDate = endDate;
        }
        
        public Guid ItemId { get; private set; }
        public Item Item { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public bool IsRecurring { get; private set; }
        public string RecurrencePattern { get; private set; }
        
        public void UpdateDates(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentException("Start date must be before end date");
                
            StartDate = startDate;
            EndDate = endDate;
        }
        
        public void SetRecurrence(string pattern)
        {
            IsRecurring = !string.IsNullOrEmpty(pattern);
            RecurrencePattern = pattern;
        }
    }
}
