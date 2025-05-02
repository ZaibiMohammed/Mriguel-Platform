using AlloVoisinClone.Application.Common.Interfaces;

namespace AlloVoisinClone.Infrastructure.Services
{
    /// <summary>
    /// Service for datetime operations
    /// </summary>
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
